using System;
using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using NPCBehaviour;

public class SimpleMonster : MonoBehaviour, Enemy
{

    [SerializeField]
    private float patrolWait;

    [SerializeField] private float attackReflex;
    
    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    private float attackDistanceStop;
    
    [SerializeField]
    private float attentionWait;

    [SerializeField]
    private float attentionDistance;

    [SerializeField]
    private Transform[] patrolPoints;

    [SerializeField]
    private EnemyState initialState;

    [SerializeField]
    private UnityStandardAssets._2D.PlatformerCharacter2D character2D;

    [SerializeField]
    private Character2dAnimationHandler animationHandler;

    [SerializeField]
    private ParticleSystem blood;
    
    private Vector2 NextPatrolPoint
    {
        get
        {
            return (Vector2)patrolPoints[++patrolIndex % patrolPoints.Length].position;
        }
    }

    private float direction;

    private int patrolIndex;

    private EnemyState state;

    public EnemyState State => state;

    private SimpleEnemyStateMachine stateMachine;

    private Transform target;

    private Agent _agent;

    private Agent Agent
    {
        get
        {
            if (_agent == null)
            {
                _agent = GetComponent<Agent>();
            }
            return _agent;
        }
    }

    private StateAttack GetAttackState()
    {
        return new StateAttack(GetComponent<Agent>(), attackReflex, attackCooldown, target);
    }

    private StateMove GetMoveLeft()
    {
        return new StateMove(Agent, -1, character2D);
    }
    
    // private StateMove GetMoveRight
    
    // Health part


    [SerializeField]
    private int maxHealth;

    private int health;

    public int CurrentHealth
    {
        get => health;
    }


    public void Attack()
    {
        animationHandler.Attack();
        // TODO -- eventualno nesto
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }

    public void Move(float move)
    {
        direction = Mathf.Clamp(move * 1000, -1, 1);
        character2D.Move(move, false, false);
    }

    private void Awake()
    {
        stateMachine = new SimpleEnemyStateMachine(GetComponent<Agent>());
    }

    private void Start()
    {
        health = maxHealth;
        patrolIndex = -1;
        state = initialState;
        try
        {
            transform.position = NextPatrolPoint;
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogError($"Nema patrol pointa za: {transform.name}, stanje se prebacuje na idle...");
            state = EnemyState.Idle;
        }
        direction = 1;
        // if (state == EnemyState.Patroling)
        // {
        //     StartCoroutine(Patroling());
        // }
        // StartCoroutine(Attention());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var cmp = other.collider.GetComponent<DamageSystem>();
        if (cmp != null)
        {
            animationHandler.Hurt();
            if (blood != null)
            {
                blood.Play();
            }
            else
            {
                Debug.LogError($"Blood not assigned for {gameObject.name}");
            }
            health = Mathf.Max(0, health - cmp.DamageAmount);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animationHandler.Die();
        enabled = false;
        StopAllCoroutines();
        var rigid2d = GetComponent<Rigidbody2D>();
        rigid2d.velocity = Vector2.zero;
        rigid2d.isKinematic = true;
        var colliders = GetComponentsInChildren<Collider2D>();
        foreach(var c in colliders)
        {
            c.enabled = false;
        }
    }

    // Coroutine for patroling
    // Coroutine for inspecting/attacking

    private IEnumerator Patroling()
    {
        Debug.Log("Entering patrol state...");        
        do
        {
            var goTo = NextPatrolPoint;
            yield return StartCoroutine(PatrolToSpot(goTo));
            if (state != EnemyState.Patroling)
            {
                break;
            }
            yield return new WaitForSeconds(patrolWait);
        }
        while(state == EnemyState.Patroling);
        Debug.Log("Exiting patrol state...");
        yield return null;
    }

    private IEnumerator Attacking(Transform t)
    {
        Debug.Log("Entering attacking state...");
        while(state == EnemyState.Attacking)
        {
            var dist = t.position.x - transform.position.x;
            var move = Mathf.Clamp(Mathf.Abs(dist) > attackDistanceStop ? dist : 0f, -1, 1);
            Move(move);
            yield return new WaitForFixedUpdate();
        }
    }

    [Button]
    private void SetAttackState()
    {
        target = FindObjectOfType<Custom2dCharacter>()?.transform;
        stateMachine.ChangeState(GetAttackState());
    }

    private bool CanAttack()
    {
        if (target == null)
        {
            return false;
        }
        return Vector2.Distance(transform.position, target.position) < 3.5f;
    }

    private bool IsNear(Vector2 spot, float delta = 1f)
    {
        return Mathf.Abs(spot.x - transform.position.x) > delta;
    }

    private IEnumerator PatrolToSpot(Vector2 spot)
    {
        while(IsNear(spot) && state == EnemyState.Patroling)
        {
            var move = Mathf.Clamp(spot.x - transform.position.x, -1, 1);
            Move(move);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
        Debug.Log("Arived on spot..stopping");
        Move(0.005f * direction);
    }

    private IEnumerator Attention()
    {
        while(enabled)
        {
            yield return new WaitForSeconds(attentionWait);

            switch(state)
            {
                case EnemyState.Attacking:
                    Attack();
                    break;
                case EnemyState.Patroling:
                    RaycastHit2D hit = Physics2D.Raycast(
                        transform.position + Vector3.up,
                        Vector2.right * direction,
                        attentionDistance,
                        1 << LayerMask.NameToLayer("Friend")
                    );
                    Debug.DrawRay(
                        transform.position + Vector3.up,
                        Vector2.right * direction * attentionDistance,
                        Color.red,
                        1f
                    );
                    if (hit.collider)
                    {
                        state = EnemyState.Attacking;
                        StartCoroutine(Attacking(hit.transform));
                    }
                    break;
                case EnemyState.Idle:
                    Debug.Log("Enemy is idle");
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
    
#if DEBUG

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(GetMoveLeft());
        }
    }

#endif
}

using UnityEngine;
using System.Collections;

public class SimpleMonster : MonoBehaviour, Enemy
{

    [SerializeField]
    private float patrolWait;

    [SerializeField]
    private float attackWait;

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
        direction = Mathf.Clamp(move * 10, -1, 1);
        character2D.Move(move, false, false);
    }

    private void Start()
    {
        patrolIndex = -1;
        state = initialState;
        transform.position = NextPatrolPoint;
        direction = 1;
        if (state == EnemyState.Patroling)
        {
            StartCoroutine(Patroling());
        }
        StartCoroutine(Attention());
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

    private IEnumerator Attacking(Transform target)
    {
        Debug.Log("Entering attacking state...");
        while(state == EnemyState.Attacking)
        {
            var dist = target.position.x - transform.position.x;
            var move = Mathf.Clamp(Mathf.Abs(dist) > attackDistanceStop ? dist : 0f, -1, 1);
            Move(move);
            yield return new WaitForFixedUpdate();
        }
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
        Move(0);
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
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}

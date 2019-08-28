using UnityEngine;
using System.Collections;

public class SimpleMonster : MonoBehaviour, Enemy
{

    [SerializeField]
    private float patrolWait;

    [SerializeField]
    private float attackWait;

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
        throw new System.NotImplementedException();
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }

    public void Move(float move)
    {
        direction = move;
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
            yield return StartCoroutine(ArrivedOnSpot(goTo));
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

    private IEnumerator ArrivedOnSpot(Vector2 spot)
    {
        while(Mathf.Abs(spot.x - transform.position.x) > 0.5f && state == EnemyState.Patroling)
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
            // FIXME -- not working now...something wrong here
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position + Vector3.up,
                Vector2.right * direction,
                attentionDistance,
                LayerMask.NameToLayer("Friend")
            );
            Debug.DrawRay(transform.position + Vector3.up, Vector2.right * direction, Color.blue, 5f);
            Debug.Log(hit.collider);            
            if (hit.collider)
            {
                state = EnemyState.Attacking;
                // TODO ...
            }
        }
    }
}

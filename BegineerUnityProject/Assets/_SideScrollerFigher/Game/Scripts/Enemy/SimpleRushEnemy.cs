using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRushEnemy : MonoBehaviour, Enemy
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Rigidbody2D rigid;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cooldown;
    private float direction;
    // privates
    private float timeLastAttackStarted;

    public void Attack()
    {
        // charge towards
        direction = 0f;
        if (target.position.x < transform.position.x)
        {
            direction = -1f;
        }
        else if (target.position.x > transform.position.x)
        {
            direction = 1f;
        }
        rigid.AddForce(new Vector2(direction, 0) * speed);
    }

    public void Inspect()
    {
        // Simple guy for now will just know
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLastAttackStarted = -cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        float now = Time.time;
        if (target != null && now > timeLastAttackStarted + cooldown)
        {
            Attack();
        }
        else
        {
            Inspect();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Die robot!");
    }

    public void Move(Vector3 to)
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float direction;
    public float speed;

    public bool useForce;
    public Rigidbody rigid;

    public int shotsToKill = 3;

    private int shootsCount = 0;

    public bool attackOnStart;

    private bool isAttacking;

    public void SetAttackingTrue()
    {
        isAttacking = true;
    }

    private void Start()
    {
        isAttacking = attackOnStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            if (!useForce)
            {
                transform.position = transform.position + new Vector3(direction, 0, 0) * speed * Time.deltaTime;
            }
            else
            {
                rigid.AddForce(new Vector3(direction, 0, 0) * speed);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Metak")
        {
            // shootsCount = shootsCount + 1;
            shootsCount++;
            if (shootsCount > shotsToKill)
            {
                Destroy(gameObject);
            }
        }
    }
}

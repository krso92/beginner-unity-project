using System;
using UnityEngine;

public class Custom2dCharacter : MonoBehaviour, Mortal
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigid2d;
    
    [SerializeField]
    private SF_PlatformerCharacter2D character2D;

    [SerializeField]
    private SF_Platformer2DUserControl controls;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float walkSpeed = 10;

    [SerializeField]
    private float runSpeed = 16;

    [SerializeField] private PolygonCollider2D aliveCollider;
    [SerializeField] private PolygonCollider2D deadCollider;
    
    private float CurrentSpeed
    {
        get => character2D.MaxSpeed;
        set => character2D.MaxSpeed = value;
    }

    private int health;

    public bool IsAlive => CurrentHealth > 0;
    
    public int CurrentHealth
    {
        get => health;
    }

    private void Awake()
    {
        FindObjectOfType<PlayerUI>().Player = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void SetSpeed(bool run)
    {
        CurrentSpeed = run ? runSpeed : walkSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
        var dmg = other.gameObject.GetComponent<DamageSystem>();
        if (dmg != null) // == means equal, != means not equal
        {
            var healthToBe = Mathf.Max(0, health - dmg.DamageAmount);
            if (healthToBe == 0)
            {
                Die();
            }
            else
            {
                health = healthToBe;
                animator.SetTrigger("Hurt");    
            }
        }
    }

    public void Die()
    {
        if (!IsAlive)
        {
            Debug.Log("Can not die while dead");
            return;
        }
        health = 0;
        animator.SetTrigger("Die");
        controls.enabled = false;
        aliveCollider.enabled = false;
        deadCollider.enabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("WizAttack"))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
        bool run = Input.GetButton("WizRun");
        SetSpeed(run);
        animator.SetBool("Run", run);
#if DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Revive");
            controls.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Die();            
        }
#endif
    }
}

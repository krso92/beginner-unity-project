using UnityEngine;

public class Custom2dCharacter : MonoBehaviour
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

    private float CurrentSpeed
    {
        get => character2D.MaxSpeed;
        set => character2D.MaxSpeed = value;
    }

    private int health;

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
        var dmg = other.gameObject.GetComponent<DamageSystem>();
        if (dmg != null) // == means equal, != means not equal
        {
            health = Mathf.Max(0, health - dmg.DamageAmount);
            animator.SetTrigger("Hurt");
        }
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        controls.enabled = false;
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
        // bool run = Input.GetButton("WizRun");
        // SetSpeed(run);
        // animator.SetBool("Run", run);
        // A little cheat ;)
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Revive");
            controls.enabled = true;
        }
    }
}

using UnityEngine;

public class Custom2dCharacter : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rigid2d;
    
    [SerializeField]
    private UnityStandardAssets._2D.Platformer2DUserControl controls;

    [SerializeField]
    private int maxHealth;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
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
            Debug.LogError("ž attak");
        }
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
        // A little cheat ;)
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Revive");
            controls.enabled = true;
        }
    }
}

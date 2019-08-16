using UnityEngine;

public class Custom2dCharacter : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private UnityStandardAssets._2D.Platformer2DUserControl controls;

    [SerializeField]
    private int maxHealth;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var dmg = other.gameObject.GetComponent<DamageSystem>();
        if (dmg != null)
        {
            health = Mathf.Max(0, health - dmg.DamageAmount);
        }
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Dead");
        controls.enabled = false;
    }

    private void Update()
    {
        // A little cheat ;)
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Revive");
            controls.enabled = true;
        }    
    }
}

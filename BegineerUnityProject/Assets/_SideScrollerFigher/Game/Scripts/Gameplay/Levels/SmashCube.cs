using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class SmashCube : MonoBehaviour
{
    [InfoBox("Limit namesten za Y u kodu")]
    [SerializeField] private float smashWait;
    [SerializeField] private Animator animator;

    private bool isKilling;
    
    private void OnEnable()
    {
        StartCoroutine(SmashLoop());
        isKilling = false;
    }

    private IEnumerator SmashLoop()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(smashWait);
            animator.SetTrigger("smash");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("back");
        }
    }

    private void Update()
    {
        if (transform.position.y < 4.052546f)
        {
            isKilling = true;
        }
        else
        {
            isKilling = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isKilling) return;
        var mortal = GetComponent<Mortal>();
        mortal?.Die();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (isKilling)
        {
            Kill(other.collider);
        }
    }

    void Kill(Collider2D creature)
    {
        var mortal = creature.gameObject.GetComponent<Mortal>();
        mortal?.Die();
    }
}

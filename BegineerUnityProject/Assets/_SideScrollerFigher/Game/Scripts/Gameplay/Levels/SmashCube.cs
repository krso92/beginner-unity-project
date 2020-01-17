using System.Collections;
using UnityEngine;

public class SmashCube : MonoBehaviour
{
    [SerializeField] private float smashWait;
    [SerializeField] private Animator animator;
    
    private void OnEnable()
    {
        StartCoroutine(SmashLoop());
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
}

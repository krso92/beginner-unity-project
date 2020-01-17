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
            // set anim, set anim
        }
    }
}

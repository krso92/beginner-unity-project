using UnityEngine;

public class Character2dAnimationHandler : MonoBehaviour
{

    [SerializeField]
    private Animator characterAnimator;
    
    [SerializeField]
    private string attackTrigger;
    [SerializeField]
    private string hurtTrigger;
    [SerializeField]
    private string dieTrigger;
    [SerializeField]
    private string walkTrigger;
    [SerializeField]
    private string idleTrigger;

    // FIXME -- use Animator.StringToHash

    public void Attack()
    {
        characterAnimator.SetTrigger(attackTrigger);
    }

    public void Hurt()
    {
        characterAnimator.SetTrigger(hurtTrigger);
    }

    public void Die()
    {
        characterAnimator.SetTrigger(dieTrigger);
    }

    public void Inspect()
    {
        characterAnimator.SetTrigger(walkTrigger);
    }

    public void Idle()
    {
        characterAnimator.SetTrigger(idleTrigger);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicaController : MonoBehaviour
{
    public Animator cicaAnimator;

    public string attackTrigger;
    public string walkTrigger;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            cicaAnimator.SetTrigger(attackTrigger);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cicaAnimator.SetBool(walkTrigger, true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            cicaAnimator.SetBool(walkTrigger, false);            
        }
    }
}

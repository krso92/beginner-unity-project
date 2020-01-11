using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTriggerEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent triggerOnEnter;

    [SerializeField]
    private UnityEvent triggerOnExit;


    private bool isPlayerInside;

    private int numInside;

    // getters

    public bool IsAnyoneInside => numInside > 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        numInside++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        numInside--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private bool inside;

    [SerializeField]
    private Collider2D elementCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        inside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inside = false;
        elementCollider.enabled = true;
    }

    private void Update()
    {
        if (Input.GetButton("GoLevelDown") && inside)
        {
            elementCollider.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider2D elementCollider;

    [SerializeField]
    private ZoneTriggerEvent zone;

    public void OnZoneExit()
    {
        elementCollider.enabled = true;
    }

    private void Update()
    {
        if (Input.GetButton("GoLevelDown") && zone.IsAnyoneInside)
        {
            elementCollider.enabled = false;
        }
    }
}

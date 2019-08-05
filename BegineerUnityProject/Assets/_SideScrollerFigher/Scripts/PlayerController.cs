using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public GameObject metakPrefab;

    public Transform aimPoint;

    private bool isFiring;

    private void Start()
    {
        Debug.Log("Hello world, " + gameObject.name + " is ready");
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            transform.position
                = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime * horizontal;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(metakPrefab, aimPoint.position, Quaternion.identity);
        }
        /*
                    -- FIXME --
        if (Input.GetAxis("Fire1") == 1 && !isFiring)
        {
            isFiring = true;
            Instantiate(metakPrefab, aimPoint.position, Quaternion.identity);
        }
        else
        {
            isFiring = false;
        }
         */
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

	public AttackWhenClose awc;
	public float speed = 5f;

	// Update is called once per frame
	
	void Update () {
		if(!awc.attacking)
			transform.position += transform.forward * speed * Time.deltaTime;
	}
}

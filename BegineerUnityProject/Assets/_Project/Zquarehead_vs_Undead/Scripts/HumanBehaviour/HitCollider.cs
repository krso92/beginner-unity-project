using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

	public int maxDamagesPerHit = 2;

	int currentHits;

	void OnEnable () {
		currentHits = 0;
	}
	
	void OnCollisionEnter(Collision coll){
		currentHits++;
		if(currentHits >= maxDamagesPerHit){
			gameObject.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	void OnCollisionEnter(Collision coll){
		if(coll.collider.tag.Equals("Hit")){
			Destroy(gameObject);
		}
	}

}

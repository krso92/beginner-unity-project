using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

	public float secsAlive;

	void OnEnable () {
		Invoke ("DisableMe", secsAlive);
	}

	void DisableMe(){
		gameObject.SetActive (false);
	}
}

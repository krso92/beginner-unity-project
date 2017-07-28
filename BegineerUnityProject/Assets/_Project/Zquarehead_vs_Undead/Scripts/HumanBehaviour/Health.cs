using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float healt;
	public Text text;

	public bool DealDamage(float dmg){
		healt -= dmg;
		if(healt <= 0f){
			text.text = healt.ToString();
			return true;
		}
		text.text = healt.ToString();
		return false;
	}
}

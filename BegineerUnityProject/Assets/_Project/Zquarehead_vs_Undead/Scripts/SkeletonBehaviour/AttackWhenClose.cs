using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWhenClose : MonoBehaviour {

	public Transform target;
	public Animator animator;
	
	public float closseness = 5f;
	public float reflex = .3f;

	public bool attacking = false;

	void Start(){
		InvokeRepeating("CheckForAttack", reflex, reflex);
		target = SkeletonController.instance.target;
	}

	void CheckForAttack() {
		// ako ne napadamo i ako smo blize od junitijevih 5 metara
		if(!attacking && Vector3.Distance(transform.position, target.position) <= closseness){
			// onda napadamo
			attacking = true;
			animator.SetTrigger("attack");
			// koliko ce attack da traje
			Invoke("AttackPassed", 1.2f);
		}
	}

	void AttackPassed(){
		attacking = false;
	}
}

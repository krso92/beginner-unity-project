using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTopDownMovement2D : MonoBehaviour {

	public float speed = 10f;
	[HideInInspector]
	public bool moving;

	public float glitchAngleAmount = 5f;
	public float glitchTime = 0.1f;

	public bool useGlitch;

	// TODO
	// sprint?

	float glitch;

	// Use this for initialization
	void Start () {
		if(useGlitch){
			glitch = glitchAngleAmount;
			InvokeRepeating("Mul", glitchTime, glitchTime);
		}
		else{
			glitch = 0f;
		}
	}
	
	void Mul(){
		glitch *= -1;
	}

	float horizontal;
	float vertical;

	// Update is called once per frame
	void Update () {
		// pokupimo input		
		horizontal = Input.GetAxis("AntHorizontal");
		vertical = Input.GetAxis("AntVertical");
		// input ose (axes) mozemo menjati ovde:
		// Edit > Project Settings > Input

		// da se zna da se krecemo
		moving = horizontal != 0 || vertical != 0;
		
		// krecemo se
		Vector3 nextPosition = ((Vector3.right * horizontal) + (Vector3.up * vertical)) * speed * Time.deltaTime;
		transform.position += nextPosition;
		
		// rotiramo mrava
		if(moving){
			// nije moguce pristupiti direktno uglu rotacije pa moramo ovako
			Quaternion rotation = new Quaternion();
			rotation.eulerAngles = new Vector3(0f, 0f, GetZRotation(horizontal, vertical) + glitch);
			transform.rotation = rotation;
		}
	}

	float GetZRotation(float horizontal, float vertical) {
		if(horizontal > 0f && vertical > 0f)
			return 45f;
		if(horizontal < 0f && vertical < 0f)
			return -135f;
		if(horizontal > 0f && vertical < 0f)
			return -45f;
		if(horizontal < 0f && vertical > 0f)
			return 135f;
		if(horizontal == 0f && vertical > 0f)
			return 90f;
		if(horizontal == 0f && vertical < 0f)
			return -90f;
		if(horizontal > 0f && vertical == 0f)
			return 0f;
		//if(horizontal < 0f && vertical == 0f)
			return 180f;
	}
}

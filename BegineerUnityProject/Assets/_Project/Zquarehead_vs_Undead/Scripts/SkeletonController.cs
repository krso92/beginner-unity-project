using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

	public static SkeletonController instance;

	public GameObject skeletonPrefab;
	public float time;
	public Transform spawnPlaces;
	public Transform target;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnSkeleton", time, time);
	}

	void SpawnSkeleton(){
		int rnd = Random.Range(0, spawnPlaces.childCount);
		Transform place = spawnPlaces.GetChild(rnd);
		GameObject tmp = Instantiate(skeletonPrefab, place);
		tmp.transform.localPosition = Vector3.zero;
	}
}

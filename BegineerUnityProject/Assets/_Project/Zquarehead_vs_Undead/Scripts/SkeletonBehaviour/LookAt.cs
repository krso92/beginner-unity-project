using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;

    public float speed;

    void Start(){
    	target = SkeletonController.instance.target;
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(target);
    }
}

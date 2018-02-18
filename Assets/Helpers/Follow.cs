using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	public GameObject target;
    public float speed;
	private Rigidbody rb;
 
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		var tp = target.transform.position;
		var op = transform.position;

		rb.AddForce((tp - op).normalized * speed * Time.smoothDeltaTime);

		// float minDiff = 0.5f;
		// var diff = tp - op;
		// if(Mathf.Abs(diff.x) > minDiff || Mathf.Abs(diff.z) > minDiff){
		// 	diff.y = 0;
		// 	rb.AddForce(diff.normalized * speed * Time.smoothDeltaTime);
		// }
		// Debug.Log(tp - op);
		// Debug.Log((tp - op).normalized);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
	public GameObject target;
    public float speed;
	private Rigidbody rb;
	GameObject player;
 
	void Start () {
		rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate () {
		var tp = target.transform.position;
		var op = transform.position;

		rb.AddForce((tp - op).normalized * speed * Time.smoothDeltaTime * player.transform.localScale.x);
	}
}

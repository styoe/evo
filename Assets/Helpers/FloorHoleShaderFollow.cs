using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHoleShaderFollow : MonoBehaviour {
	private GameObject player;
	
	void Start () {
		player = GameObject.Find("Player");
	}
	
	void FixedUpdate () {
		Vector4 pos = player.gameObject.transform.position;
		pos.w = pos.y * 10f;
		pos.y = 0;
		gameObject.GetComponent<Renderer>().material.SetVector("_HolePos", pos);
	}
}

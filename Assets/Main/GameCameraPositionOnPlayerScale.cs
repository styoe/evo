using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraPositionOnPlayerScale : MonoBehaviour {
	private GameObject mainCamera;
	private GameObject player;
	
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		player = GameObject.Find("Player");
	}
	
	void Update () {
		float modifier = 5f;
		var playerScale = Mathf.Round(player.transform.localScale.x);
		var cameraY = mainCamera.transform.position.y;
		int cameraBaseY = 10;

		Debug.Log(mainCamera.transform);

		if(cameraY < playerScale * modifier){
			mainCamera.transform.position += Vector3.up;
		}
	}
}

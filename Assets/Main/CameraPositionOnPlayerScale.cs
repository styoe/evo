using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionOnPlayerScale : MonoBehaviour {
	private GameObject mainCamera;

	private GameObject spotLight;
	private Light spotLightLight;
	
	private GameObject VisionLight;
	private Light visionLightLight;
	
	private GameObject player;
	
	void Start () {
		mainCamera = GameObject.Find("Camera");

		spotLight = GameObject.Find("SpotLight");
		spotLightLight = spotLight.GetComponent<Light>();

		VisionLight = GameObject.Find("VisionLight");
		visionLightLight = VisionLight.GetComponent<Light>();

		player = GameObject.Find("Player");
	}
	
	void Update () {
		var cameraY = mainCamera.transform.position.y;
		var spotY = spotLight.transform.position.y;
		var playerScale = Mathf.Round(player.transform.localScale.x);

		if(cameraY < playerScale * 3){
			Animate.PositionAdd(mainCamera, new Vector3(0, 0.1f, 0));
		}

		if(spotY < playerScale * 3 - playerScale * 0.1 ){
			Animate.PositionAdd(spotLight, new Vector3(0, 0.1f, 0));
			Animate.PositionAdd(VisionLight, new Vector3(0, 0.1f, 0));

			spotLightLight.range = cameraY + 5;
			visionLightLight.range = cameraY + 35;
			Animate.PositionAdd(spotLight, new Vector3(0, 0.1f, 0));			
		}
	}

	static private IEnumerator TransformRange(Light light, int value, float duration){
        float elapsedTime = 0;

        while (elapsedTime <= duration)
        {
            light.range = value * elapsedTime / duration;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        light.range = value;
    }
}

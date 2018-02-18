using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public GameObject cookie;
	private int maxCookies;

	public GameObject powerup;
	private int maxPowerups;
	string[] powerups = new string[] {"Fly", "Jump"};

	void Start () {
		maxCookies = 40;
		InvokeRepeating("CreateCookie", 2.0f, 0.1f);
		
		maxPowerups = 10;
		InvokeRepeating("CreatePowerup", 2.0f, 2f);
	}

	void CreateCookie () {
		if(GameObject.FindGameObjectsWithTag("Cookie").GetLength(0) < maxCookies){
			var newCookie = Instantiate(cookie, new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
			newCookie.name = "Cookie";
			newCookie.tag = "Cookie";

			float cookieScale = Random.Range(0.3f, 3f);
			newCookie.transform.localScale = new Vector3(cookieScale, cookieScale, cookieScale);

			var cookieSphereCollider = newCookie.gameObject.GetComponent<SphereCollider>(); 
			Debug.Log(cookieScale);
			// cookieSphereCollider.radius = cookieScale / 2f; 
		}
	}

	void CreatePowerup () {
		if(GameObject.FindGameObjectsWithTag("Powerup").GetLength(0) < maxPowerups){
			var newPowerup = Instantiate(powerup, new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)), Quaternion.identity);
			var powerupType = powerups[Random.Range(0, powerups.Length)];
			newPowerup.name = powerupType;
			newPowerup.tag = "Powerup";
			newPowerup.transform.rotation = new Quaternion(-90, 0, 0, 0);

			var textMesh = newPowerup.GetComponent<TextMesh>();
			textMesh.text = powerupType;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


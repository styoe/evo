using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public GameObject cookie;
	private int maxCookies;

	public GameObject powerup;
	private int maxPowerups;
	string[] powerups = new string[] {"Fly", "Jump"};

	public GameObject rock;
	private int maxRocks;

	public GameObject bomb;
	private int maxBombs;

	public GameObject player;

	public GameObject floor;

	public float GameGridRadius;

	void Start () {
		GameGridRadius = 20;

		maxCookies = 100;
		InvokeRepeating("CreateCookie", 2.0f, 0.3f);
		InvokeRepeating("CreateCookie", 2.0f, 2f);
		
		maxPowerups = 20;
		InvokeRepeating("CreatePowerup", 2.0f, 0.3f);
		InvokeRepeating("CreatePowerup", 2.0f, 2f);

		maxRocks = 200;
		InvokeRepeating("CreateRock", 2.0f, 0.03f);
		InvokeRepeating("CreateRock", 2.0f, 2f);

		maxBombs = 30;
		InvokeRepeating("CreateBomb", 2.0f, 0.03f);
		InvokeRepeating("CreateBomb", 2.0f, 10f);

		var ExplosionCenter = new Vector3(2, 2, 2);	
		float ExplosionRange = 10f;
		Collider[] colliders = Physics.OverlapSphere(ExplosionCenter, ExplosionRange);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(500f, ExplosionCenter, ExplosionRange, 100.0F);
        }
	}

	void updateFixed () {
		Debug.Log(player.transform.localScale.x);
		Debug.Log(GameGridRadius);

		if(player.transform.localScale.x * GameGridRadius > floor.transform.localScale.x){
			var floorScale = player.transform.localScale.x * GameGridRadius * 2;
			floor.transform.localScale = new Vector3(floorScale, floorScale, floorScale);
		}
	}

	void CreateCookie () {
		if(GameObject.FindGameObjectsWithTag("Cookie").GetLength(0) < maxCookies) {
			var playerScale = player.transform.localScale.x;
			var newCookie = Instantiate(cookie, new Vector3(Random.Range(-1 * GameGridRadius * playerScale, 20*playerScale), 1, Random.Range(-20*playerScale, 20*playerScale)), Quaternion.identity);
			newCookie.name = "Cookie";
			newCookie.tag = "Cookie";

			float cookieScale = Random.Range(0.3f*playerScale, 3f*playerScale);
			Animate.Scale(newCookie, new Vector3(cookieScale, cookieScale, cookieScale));

			// var cookieSphereCollider = newCookie.gameObject.GetComponent<SphereCollider>(); 
			// Debug.Log(cookieScale);
			// cookieSphereCollider.radius = cookieScale / 2f; 
		}
	}

	void CreatePowerup () {
		if(GameObject.FindGameObjectsWithTag("Powerup").GetLength(0) < maxPowerups){
			var playerScale = player.transform.localScale.x;
			var newPowerup = Instantiate(powerup, new Vector3(Random.Range(-1 * GameGridRadius, GameGridRadius), 1, Random.Range(-1 * GameGridRadius, GameGridRadius)), Quaternion.identity); 
			var powerupType = powerups[Random.Range(0, powerups.Length)];
			newPowerup.name = powerupType;
			newPowerup.tag = "Powerup";
			newPowerup.transform.rotation = new Quaternion(-90, 0, 0, 0);

			var textMesh = newPowerup.GetComponent<TextMesh>();
			textMesh.text = powerupType;

			float powerupScale = Random.Range(0.3f*playerScale, 3f*playerScale);
			Animate.Scale(newPowerup, new Vector3(powerupScale, powerupScale, powerupScale));
		}
	}

	void CreateRock () {
		var playerScale = player.transform.localScale.x;
		var rocks = GameObject.FindGameObjectsWithTag("Rock");
		var sizeDeviation = 3f;
		
		// If less rocks than maxRocks, set destroy on the smallest
		if(rocks.GetLength(0) >= maxRocks) {
			GameObject smallestRock = new GameObject();
			RockScript smallestRockScript;
			bool smallestRockExists  = false;

			foreach (GameObject rock in rocks) {
				if(rock.transform.localScale.x < smallestRock.transform.localScale.x){
					smallestRock = rock;
					smallestRockScript = smallestRock.GetComponent<RockScript>();
					smallestRockExists = true;
				}
			}

			if(smallestRockExists) {
				smallestRockScript.Destroy();
			}
		}

		// Create a rock
		if(rocks.GetLength(0) < maxRocks) {
			var newRock = Instantiate(rock, new Vector3(Random.Range(-1 * GameGridRadius * playerScale, GameGridRadius * playerScale), 1, Random.Range(-1 * GameGridRadius * playerScale / 5, GameGridRadius * playerScale  / 5)), Quaternion.identity);
			newRock.tag = "Rock";
			newRock.name = "Rock";

			float rockScale = Random.Range(1f * sizeDeviation * playerScale, 3f*playerScale);
			Animate.Scale(newRock, new Vector3(rockScale, rockScale, rockScale));
		}
	}

	void CreateBomb () {
		if(GameObject.FindGameObjectsWithTag("Bomb").GetLength(0) < maxBombs){
			var playerScale = player.transform.localScale.x;
			// var newBomb = Instantiate(bomb, new Vector3(Random.Range(-3*playerScale, 3*playerScale), 1, Random.Range(-3*playerScale, 3*playerScale)), Quaternion.identity);
			var newBomb = Instantiate(bomb, new Vector3(Random.Range(-1 * GameGridRadius * playerScale, GameGridRadius * playerScale), 1, Random.Range(-1 * GameGridRadius * playerScale, GameGridRadius * playerScale)), Quaternion.identity);
			newBomb.tag = "Bomb";
			newBomb.name = "Bomb";

			float bombScale = Random.Range(0.3f*playerScale, 1.5f*playerScale);
			Animate.Scale(newBomb, new Vector3(bombScale, bombScale, bombScale));
		}
	}
}


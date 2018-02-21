using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {
	public bool isTicking;
	// Use this for initialization
	void Start () {
		isTicking = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explode(){
		isTicking = true;
		StartCoroutine(ExplodeRoutione());
	}

	public IEnumerator ExplodeRoutione(){
		float duration = 3f;
		float initScale = gameObject.transform.localScale.x;

		float phase1Duration= duration * 0.5f; // Percent of duration
		float phase1Pulse = 0.5f; // Speed of pulse

		for (float i = 0; i < phase1Duration; i += phase1Pulse)
		{
			yield return Pulse(gameObject, initScale * 0.2f, phase1Pulse);
			yield return new WaitForSeconds(phase1Pulse);
		}

		float phase2Duration= duration * 0.5f; // Percent of duration
		float phase2Pulse = 0.3f; // Speed of pulse

		for (float i = 0; i < phase2Duration; i += phase2Pulse)
		{
			yield return Pulse(gameObject, initScale * 0.4f, phase2Pulse);
			yield return new WaitForSeconds(phase2Pulse);
		}
		
		
		Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 10f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(1000f, gameObject.transform.position, 10f, 300.0F);
        }

        Destroy(gameObject);
	}

	// Scale up then down by a value
	public IEnumerator Pulse(GameObject obj, float scale, float speed){
		var s = new Vector3(scale, scale, scale);
		var initScale = obj.transform.localScale;
		var plusScale = initScale + s;
		var minusScale = initScale - s;

		Animate.Scale(obj, minusScale, speed / 4f);
		yield return new WaitForSeconds(speed/ 4f);

		Animate.Scale(obj, plusScale, speed / 2f);
		yield return new WaitForSeconds(speed / 2f);

		Animate.Scale(obj, initScale, speed / 4);
		yield return new WaitForSeconds(speed / 4f);

		obj.transform.localScale = initScale;
	}
}




		// iTween.PunchScale(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
		// yield return new WaitForSeconds(0.5f);


		// float elapsedTime = 0;
		// float totalDuration = 0.5f;

		// while (elapsedTime <= totalDuration)
        // {
        //     elapsedTime += Time.deltaTime;

		// 	if(elapsedTime < totalDuration * 0.3) {
		// 		Debug.Log("SLOW");
		// 		iTween.PunchScale(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
		// 		yield return new WaitForSeconds(0.5f);
		// 		continue;
		// 	}

		// 	if(elapsedTime < totalDuration * 0.7) {
		// 		Debug.Log("MID");
		// 		iTween.PunchScale(gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
		// 		yield return new WaitForSeconds(0.2f);
		// 		continue;	
		// 	}

		// 	if(elapsedTime < totalDuration) {
		// 		Debug.Log("FAST");
		// 		iTween.PunchScale(gameObject, new Vector3(0.3f, 0.3f, 0.3f), 0.1f);
		// 		yield return new WaitForSeconds(0.1f);	
		// 		continue;
		// 	}
		// }
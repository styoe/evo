using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {
	private Rigidbody rb;
    private enum JumpPhases {Idle, Up, Down};
    private JumpPhases jumpPhase;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
        jumpPhase = JumpPhases.Idle;

		// Remove Jump if exists
		var jump = this.gameObject.GetComponent("Jump");
		if(jump){
			Destroy(jump);
		}
	}
	
	void FixedUpdate () {
        if (Input.GetKeyDown("space") && jumpPhase == JumpPhases.Idle){
            StartCoroutine(JumpPhaseChanger());
			Vector3 movement = new Vector3 (0, 300, 0);
        	rb.AddForce (movement);
        }
	}
	
    IEnumerator JumpPhaseChanger() {   
        jumpPhase = JumpPhases.Up;
        yield return new WaitForSeconds(0.2f);
        jumpPhase = JumpPhases.Down;
        yield return new WaitForSeconds(0.2f);
        jumpPhase = JumpPhases.Idle;
    }
}

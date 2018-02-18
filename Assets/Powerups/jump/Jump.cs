using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
	
    private Rigidbody rb;
    private enum JumpPhases {Idle, Up, Down};
    private JumpPhases jumpPhase;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        jumpPhase = JumpPhases.Idle;

        var fly = this.gameObject.GetComponent("Fly");
		if(fly){
			Destroy(fly);
		}
	}
	
	// Update is called once per frame
	void Update () {
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
        yield return new WaitForSeconds(0.6f);
        jumpPhase = JumpPhases.Idle;
    }
}

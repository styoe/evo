using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed;
    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
		speed = 10;

        
		// AnimationHelper.Animate(Time.time, 3.0f, (t) => {
		// 	transform.localPosition = Vector3.Lerp(Vector3.zero, 100f * Vector3.one, EasingFunctions.easeOut(t));
		// });
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0f, moveVertical);
        rb.AddForce (movement * speed);
    }
}
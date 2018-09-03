using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	float speedForce = 10f;
	float torqueForce = 200f;
	float driftFactor = 0.999f;
	float driftFactorSticky = 0.9f;
	float driftFactorSlippy = 1f;
	float maxStickyVelocity = 2.5f;
	float minSlippyVelocity = 1.5f;

	// Use this for initialization
	void Start () {
		Debug.Log("Game Started");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		float driftFactor = driftFactorSticky;

		if (RightVelocity().magnitude > maxStickyVelocity){
			driftFactor = driftFactorSlippy;
		}

		if(Input.GetButton("Accelerate")){
			rb.AddForce(transform.up * speedForce);
		}

		rb.angularVelocity = Input.GetAxis("Horizontal") * torqueForce;
		
		rb.velocity = ForwardVelocity() + RightVelocity()*driftFactor;

		float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);


	}

	Vector2 ForwardVelocity() {
		return transform.up * Vector2.Dot( GetComponent<Rigidbody2D>().velocity, transform.up);
	}

	Vector2 RightVelocity() {
		return transform.right * Vector2.Dot( GetComponent<Rigidbody2D>().velocity, transform.right);
	}
}

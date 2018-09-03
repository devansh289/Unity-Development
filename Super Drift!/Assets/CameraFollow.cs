using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.transform.position;
		offset.z = -10;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position + offset;

	}
}

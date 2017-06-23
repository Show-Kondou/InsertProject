using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public Transform m_Target;
	public float speed;
	private Vector3 FirstPosition;

	// Use this for initialization
	void Start () {
		FirstPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)) {
			ZoomMove();
		}
		if(Input.GetKey(KeyCode.X)) {
			OutMove();
		}
	}


	private void ZoomMove() {
		var pos = transform.position;
		var sub = (m_Target.position - pos) / 8.0F * Time.deltaTime * speed;
		pos += sub;
		if(pos.z > -2.0f)
			pos.z = -2.0f;
		transform.position = pos;
	}


	private void OutMove() {
		var pos = transform.position;
		var sub = (FirstPosition - pos) / 8.0F * Time.deltaTime * speed;
		pos += sub;
		transform.position = pos;
	}
}

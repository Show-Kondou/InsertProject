﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public Transform m_Target;
	public float speed;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Z ) ) {
			ZoomMove();
		}
	}


	private void ZoomMove() {
		var pos = transform.position;
		var sub = (m_Target.position - pos) / 8.0F * Time.deltaTime * speed;
		pos += sub;
		transform.position = pos;
	}
}

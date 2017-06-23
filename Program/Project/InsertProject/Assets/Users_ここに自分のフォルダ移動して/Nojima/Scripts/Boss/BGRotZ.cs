using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRotZ : MonoBehaviour {
    [SerializeField]
    float RotZSpeed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles -= new Vector3(0f, 0f, RotZSpeed * Time.deltaTime);

	}
}

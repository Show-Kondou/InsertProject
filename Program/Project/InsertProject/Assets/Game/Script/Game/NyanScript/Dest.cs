using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dest : MonoBehaviour {

    private float i;

    public float fDestLimit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        i += Time.deltaTime;

        if (fDestLimit < i)
            Destroy(this.gameObject);
    }
}

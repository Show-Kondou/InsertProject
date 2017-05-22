using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour {

    [SerializeField]
    float ScrollSpeed = 10f;
    MeshRenderer meshRender;

    float Offset = 0f;

	// Use this for initialization
	void Start () {
		meshRender = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Offset = Mathf.Repeat(Time.time * ScrollSpeed, 1f);

        meshRender.material.SetTextureOffset("_MainTex", new Vector2(0f, Offset));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CSSandwichObjManager.Instance.CreateSandwichObj(CSSandwichObjManager.SandwichObjType.BOSS, new Vector2(0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

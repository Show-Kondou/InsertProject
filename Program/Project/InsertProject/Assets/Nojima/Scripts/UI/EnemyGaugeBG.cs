using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGaugeBG : EnemyGauge
{
    // Use this for initialization
    void Start () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(MAX_HP, GetComponent<RectTransform>().sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

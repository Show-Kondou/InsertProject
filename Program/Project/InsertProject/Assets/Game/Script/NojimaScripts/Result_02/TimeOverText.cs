using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOverText : MonoBehaviour {

    float MySize = 1f;
    bool bScaler = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(MySize <= 1f)
            bScaler = true;
        if(MySize >= 1.1f)
            bScaler = false;

        if (bScaler)
        {
            MySize += 0.1f * Time.deltaTime;
        }
        else
            MySize -= 0.1f * Time.deltaTime;

        transform.localScale = new Vector3(MySize, MySize);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour {

    float Minute = 0f;  //分
    float Seconds = 0f; //秒

    // Use this for initialization
    void Start () {
        GetComponent<Text>().text = Minute.ToString("00") + ":" + Seconds.ToString("00");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour 
{
    private TimeLimit _TimeLimit;   // 制限時間のスクリプト用

	// ===== スタート関数 =====
	void Start () 
    {
        _TimeLimit = GameObject.Find("Center").GetComponent<TimeLimit>();
	}
	
	// ===== 更新関数
	void Update () 
    {
        this.GetComponent<Text>().text = _TimeLimit.GetLimitTime().ToString("f1");
	}
}

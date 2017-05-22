using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour {

    public GameObject LinePrefab;

    public int nLineNum;

	// Use this for initialization
	void Start () {

        nLineNum = 0;

    }
	
	// Update is called once per frame
	void Update () {


        //トリガー時処理
        if (Input.GetMouseButtonDown(0))
        {
            nLineNum++;

            GameObject Line =  Instantiate(LinePrefab) as GameObject;

            Line.name = "Line" + nLineNum;

            Line.GetComponent<line>().nLineID = nLineNum;

        }


    }
}

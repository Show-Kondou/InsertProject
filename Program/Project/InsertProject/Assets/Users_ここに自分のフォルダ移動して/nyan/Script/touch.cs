using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour {

    //プレファブ
    public GameObject LinePrefab;

    //ライン連番
    public int nLineNum;

	// Use this for initialization
	void Start () {

        nLineNum = 100;

    }
	
	// Update is called once per frame
	void Update () {


        //トリガー時処理
        if (Input.GetMouseButtonDown(0))
        {
            nLineNum = nLineNum + 10; ;

            //ラインプレファブ生成
            GameObject Line =  Instantiate(LinePrefab) as GameObject;
            Line.name = "Line" + nLineNum;

            //ラインにID付与
            Line.GetComponent<line>().nLineID = nLineNum;

        }


    }
}

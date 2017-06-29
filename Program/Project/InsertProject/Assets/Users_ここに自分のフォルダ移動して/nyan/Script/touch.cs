using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour {

    //プレファブ
    public GameObject LinePrefab;

    //ライン連番
    public int nLineNum;

    //ぬるぬるカウンター nSlimyCnt touch.cs用
    public int nSTouchCntS = 0;
    public int nSTouchCntE = 0;

    public bool bStickyS;
    public bool bStickyE;


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

            //ぬるぬる判定
            if (0 < nSTouchCntS)
            {
                Line.GetComponent<line>().bSLineS = true;
                nSTouchCntS -= 1;
            }
            else
            {
                Line.GetComponent<line>().bSLineS = false;
            }

            if (0 < nSTouchCntE)
            {
                Line.GetComponent<line>().bSLineE = true;
                nSTouchCntE -= 1;
            }
            else
            {
                Line.GetComponent<line>().bSLineE = false;
            }

            //ラインにID付与
            Line.GetComponent<line>().nLineID = nLineNum;

        }

        bStickyS = false;
        bStickyE = false;

    }
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class line : MonoBehaviour {

    //ゲームオブジェクト
    private LineRenderer lRendere;
    public GameObject CameraObj;
    public GameObject PressPrefab;
    public GameObject PointPrefab;

    //カメラ座標
    private Vector3 vCameraPos = new Vector3(0, 0, 0);

	//マウス座標
	private Vector3 vSMousePos;     //スクリーン
	private Vector3 vWMousePos;     //ワールド

    //差分計算用
    public bool bDiffCheck = false;
    private Vector3 vNewPos;
    private Vector3 vOldPos;
    private float   fDiffX;
    private float   fDiffY;

    //始点終点
    private Vector3 vStartPos;      // マウスを押した位置を格納
	private Vector3 vEndPos;        // マウスを離した位置を格納

    //頂点格納用
    public List<Vector3> lvPointStorage = new List<Vector3>();
    private int nPointCnt = 1;

    //マウスチェックフラグ
    public bool bFirstClick;
    public bool bReleaseCheck;

	private Vector3 initialPosition;

    //Press召喚座標
    Vector3 vStartPress;
    Vector3 vEndPress;


    private Vector3 StartPosition;  // マウスを押した位置を格納
    private Vector3 EndPosition;    // マウスを離した位置を格納

    //タッチオブジェクト呼び出し
    GameObject TouchObj;
    touch _touch;

    

    //------

    void Start()
	{

        lRendere = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        lRendere.SetWidth(0.05f, 0.05f);

        bFirstClick = false;
        bReleaseCheck = false;


        //アップデートと同じ処理
        //カメラ座標
        initialPosition = new Vector3(0.0f, 1.0f, -10.0f);
        vCameraPos = initialPosition;

        //Old座標に代入
        vOldPos = vSMousePos;

        // Vector3でマウス位置座標を取得する
        vSMousePos = Input.mousePosition;
        // Z軸修正
        vSMousePos.z = -vCameraPos.z;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        vWMousePos = Camera.main.ScreenToWorldPoint(vSMousePos);

        //New座標に代入
        vNewPos = vSMousePos;
        bDiffCheck = true;

        //タッチオブジェクト呼び出し
        TouchObj = GameObject.Find("Touch");
        _touch = TouchObj.GetComponent<touch> ();

    }

    void Update () 
	{

        if (bReleaseCheck == false)
        {

            //カメラ座標
            initialPosition = new Vector3(0.0f, 1.0f, -10.0f);
            vCameraPos = initialPosition;

            ////Old座標に代入
            //vOldPos = vSMousePos;

            // Vector3でマウス位置座標を取得する
            vSMousePos = Input.mousePosition;
            // Z軸修正
            vSMousePos.z = -vCameraPos.z;
            // マウス位置座標をスクリーン座標からワールド座標に変換する
            vWMousePos = Camera.main.ScreenToWorldPoint(vSMousePos);

            //New座標に代入
            vNewPos = vWMousePos;
            bDiffCheck = true;



            ////トリガー時処理
            //if (Input.GetMouseButtonDown(0))
            //{
            //    //ダウン時格納場所を作り直す
            //    nPointCnt = 1;
            //    lRendere.SetVertexCount(nPointCnt);

            //    //マウスクリック時の座標を格納
            //    vStartPos = vWMousePos;
            //    lRendere.SetPosition(0, vStartPos);
            //    lvPointStorage.Clear();
            //    lvPointStorage.Add(vStartPos);
            //}

            //ホールド時処理
            if (Input.GetMouseButton(0))
            {
                //初回クリック時処理
                if (bFirstClick == false)
                {

                    //ダウン時格納場所を作り直す
                    nPointCnt = 1;
                    lRendere.SetVertexCount(nPointCnt);

                    //マウスクリック時の座標を格納
                    vStartPos = vWMousePos;
                    lRendere.SetPosition(0, vStartPos);
                    lvPointStorage.Clear();
                    lvPointStorage.Add(vStartPos);
                    vOldPos = vWMousePos;

                    //クリックチェック切り替え
                    bFirstClick = true;

                }
                else if (bFirstClick == true)
                {
                    
                    if (bDiffCheck == true)
                    {

                        //差分計算
                        if (0 < vOldPos.x)
                        {
                            if (0 < vNewPos.x)
                                fDiffX = vOldPos.x - vNewPos.x;

                            if (vNewPos.x < 0)
                                fDiffX = vOldPos.x - (vNewPos.x * -1.0f);
                        }

                        if (vOldPos.x < 0)
                        {
                            if (0 < vNewPos.x)
                                fDiffX = (vOldPos.x * -1.0f) - vNewPos.x;

                            if (vNewPos.x < 0)
                                fDiffX = (vOldPos.x * -1.0f) - (vNewPos.x * -1.0f);
                        }

                        if (0 < vOldPos.y)
                        {
                            if (0 < vNewPos.y)
                                fDiffY = vOldPos.y - vNewPos.y;

                            if (vNewPos.y < 0)
                                fDiffY = vOldPos.y - (vNewPos.y * -1.0f);
                        }

                        if (vOldPos.y < 0)
                        {
                            if (0 < vNewPos.y)
                                fDiffY = (vOldPos.y * -1.0f) - vNewPos.y;

                            if (vNewPos.x < 0)
                                fDiffY = (vOldPos.y * -1.0f) - (vNewPos.y * -1.0f);
                        }

                        //
                        if (fDiffX < 0)
                        {
                            fDiffX = fDiffX * -1.0f;
                        }
                        if (fDiffY < 0)
                        {
                            fDiffY = fDiffY * -1.0f;
                        }


                        //一定量移動で処理
                        float fDiffAbout = 0.5f;
                        if (fDiffAbout < fDiffX || fDiffAbout < fDiffY)//仮
                        {
                            //一定量移動時格納場所を増やす
                            nPointCnt += 1;
                            lRendere.SetVertexCount(nPointCnt);

                            //移動時の座標を格納
                            lRendere.SetPosition(nPointCnt - 1, vWMousePos);
                            lvPointStorage.Add(vWMousePos);

                            //ポイントごとにPointObjを表示(デバッグ用)
                            GameObject gStartPress = Instantiate(PointPrefab, vWMousePos, transform.rotation) as GameObject;


                            Color c1 = new Color(1, 1, 0, 1);
                            Color c2 = new Color(0, 1, 1, 1);

                            lRendere.SetColors(c1, c2);

                            //新しい点を描画したらvOldPosに代入
                            vOldPos = vWMousePos;

                        }

                        bDiffCheck = false;


                    }
                }
            }

            //リリース時処理
            if (Input.GetMouseButtonUp(0)/* && FIrstDown == true*/)
            {
                //アップ時格納場所を増やす
                nPointCnt += 1;
                lRendere.SetVertexCount(nPointCnt);

                //マウスリリース時の座標を格納
                vEndPos = vWMousePos;
                lRendere.SetPosition(nPointCnt - 1, vEndPos);
                lvPointStorage.Add(vEndPos);

                //リリースしたタイミングで壁を召喚
                vStartPress = lvPointStorage[0];
                GameObject gStartPress = Instantiate(PressPrefab, vStartPress, transform.rotation) as GameObject;
                gStartPress.name = "startPress" + _touch.nLineNum;
                gStartPress.transform.parent = transform;
                gStartPress.GetComponent<Press>().bWallStart = true;

                vEndPress = lvPointStorage[nPointCnt - 1];
                GameObject gEndPress = Instantiate(PressPrefab, vEndPress, transform.rotation) as GameObject;
                gEndPress.name = "endPress" + _touch.nLineNum;
                gEndPress.transform.parent = transform;
                gEndPress.GetComponent<Press>().bWallStart = false;

                //リリースチェックをtrueにして線を引けないようにする
                bReleaseCheck = true;
            }

        }
    }

}

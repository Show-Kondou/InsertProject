﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class line : MonoBehaviour {

    //ID
    public int nLineID;

    //ゲームオブジェクト
    private LineRenderer lRendere;
    public GameObject gPressPrefab;
    public GameObject gPointPrefab;

    //カメラ座標
    private Vector3 vCameraPos = new Vector3(0, 0, 0);
    private Vector3 initialPosition;

    //マウス座標
    private Vector3 vSMousePos;     //スクリーン
	private Vector3 vWMousePos;     //ワールド

    //差分計算用
    private bool bDiffCheck = false;
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
    private bool bFirstClick;
    private bool bReleaseCheck;

    //Press召喚座標
    Vector3 vStartPress;
    Vector3 vEndPress;

    //タッチオブジェクト呼び出し
    GameObject TouchObj;
    touch _touch;
    private int nPressNum;

    //距離計算
    public float fDistanceTotal;
    public float fDistanceLimit;

    //線の色
    public Color c1 = new Color(0, 0, 0, 1);
    public Color c2 = new Color(0, 0, 0, 1);

    //初回生成時移動許可判定
    public bool bFirstMove = false;

    //レイ
    public LayerMask layerMask;
    private Ray rPointToRay;
    private RaycastHit rHitInfo;
    private Vector3 vRayPos;


    //ぬるぬるカウンター nSlimyCnt line.cs用
    public bool bSLineS;
    public bool bSLineE;

    //強制リリース
    public bool bForcedUp = false;

    //------

    void Start()
	{
        //LineRenderer生成
        lRendere = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        lRendere.SetWidth(0.25f, 0.25f);
        //線の色
        lRendere.SetColors(c1, c2);

        //タッチ判定
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

            // Vector3でマウス位置座標を取得する
            vSMousePos = Input.mousePosition;

            //// Z軸修正
            vSMousePos.z = 4.0f;

            // マウス位置座標をスクリーン座標からワールド座標に変換する
            vWMousePos = Camera.main.ScreenToWorldPoint(vSMousePos);
            //vWMousePos.z = 0.0f;
            //vWMousePos = vSMousePos;


            //レイ
            rPointToRay = Camera.main.ScreenPointToRay(vSMousePos);

            rHitInfo = new RaycastHit();
            if (Physics.Raycast(rPointToRay, out rHitInfo, 30.0f, layerMask.value))
            {
                vRayPos = rHitInfo.point;
            }
            
            Debug.DrawRay(rPointToRay.origin, rPointToRay.direction * 30.0f);




            //New座標に代入
            vNewPos = vRayPos;
            bDiffCheck = true;

            //ホールド時処理
            if (Input.GetMouseButton(0) && bForcedUp == false) {
				//初回クリック時処理
				if (bFirstClick == false)
                {

                    //ダウン時格納場所を作り直す
                    nPointCnt = 1;
                    lRendere.SetVertexCount(nPointCnt);

                    //マウスクリック時の座標を格納
                    vStartPos = vRayPos;
                    lRendere.SetPosition(0, vStartPos);
                    lvPointStorage.Clear();
                    lvPointStorage.Add(vStartPos);
                    vOldPos = vRayPos;

                    //クリックチェック切り替え
                    bFirstClick = true;

                }
                else if (bFirstClick == true) {
					if (bDiffCheck == true)
                    {
                        //差分計算(値を正の数にする)
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

                            if (vNewPos.y < 0)
                                fDiffY = (vOldPos.y * -1.0f) - (vNewPos.y * -1.0f);
                        }

                        //差分計算
                        if (fDiffX < 0)
                        {
                            fDiffX = fDiffX * -1.0f;
                        }
                        if (fDiffY < 0)
                        {
                            fDiffY = fDiffY * -1.0f;
                        }


                        //一定量移動で処理
                        float fDiffAbout = 0.0625f;
                        if (fDiffAbout < fDiffX || fDiffAbout < fDiffY)//仮
                        {
							
							//一定量移動時格納場所を増やす
							nPointCnt += 1;
                            lRendere.SetVertexCount(nPointCnt);
							CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_MAGICWALL_POP);


							//移動時の座標を格納
							lRendere.SetPosition(nPointCnt - 1, vRayPos);
                            lvPointStorage.Add(vRayPos);

                            ////ポイントごとにPointObjを表示(デバッグ用)
                            //GameObject gStartPress = Instantiate(PointPrefab, vWMousePos, transform.rotation) as GameObject;

                            //距離を加算
                            fDistanceTotal += (vOldPos - vRayPos).magnitude;
                            //Debug.Log((vOldPos - vWMousePos).magnitude);

                            //ラインの最大距離を引いたら強制リリース
                            if (fDistanceLimit <= fDistanceTotal)
                            {
                                bForcedUp = true;
                            }


                            //線の色を設定
                            lRendere.SetColors(c1, c2);

                            //新しい点を描画したらvOldPosに代入
                            vOldPos = vRayPos;

                        }

                        bDiffCheck = false;


                    }
                }

				//if (nPointCnt == 3)
				//{
				//    //最初の壁を召喚
				//    vStartPress = lvPointStorage[0];
				//    GameObject gStartPress = Instantiate(PressPrefab, vStartPress, transform.rotation) as GameObject;
				//    gStartPress.name = "startPress" + _touch.nLineNum;
				//    gStartPress.tag = "StartPress";
				//    gStartPress.transform.parent = transform;
				//    gStartPress.GetComponent<Press>().bWallStart = true;
				//    gStartPress.GetComponent<Press>().nPressID = nLineID;
				//}
				
			}

            //リリース時処理
            if (Input.GetMouseButtonUp(0))
            {
                //線の長さが基準値に達していなかったら線を引かずオブジェクトも消す。
                if (lvPointStorage.Count < 5 || fDistanceTotal < 1.0f )
                {
                    this.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                }
                ////アップ時格納場所を増やす
                //nPointCnt += 1;
                //lRendere.SetVertexCount(nPointCnt);

                ////マウスリリース時の座標を格納
                //vEndPos = vRayPos;
                //lRendere.SetPosition(nPointCnt - 1, vEndPos);
                //lvPointStorage.Add(vEndPos);

                //ラインIDをプレスIDに代入
                nPressNum = _touch.nLineNum;

                //最初の壁を召喚
                vStartPress = lvPointStorage[0];
                GameObject gStartPress = Instantiate(gPressPrefab, vStartPress, transform.rotation) as GameObject;
                nPressNum++;
                gStartPress.name = "startPress" + nPressNum;
                gStartPress.tag = "StartPress";
                gStartPress.transform.parent = transform;
                gStartPress.GetComponent<Press>().bWallStart = true;
                gStartPress.GetComponent<Press>().nPressID = nLineID;

                //終わりの壁を召喚
                vEndPress = lvPointStorage[nPointCnt - 1];
                GameObject gEndPress = Instantiate(gPressPrefab, vEndPress, transform.rotation) as GameObject;
                nPressNum++;
                gEndPress.name = "endPress" + nPressNum;
                gEndPress.tag = "EndPress";
                gEndPress.transform.parent = transform;
                gEndPress.GetComponent<Press>().bWallStart = false;
                gEndPress.GetComponent<Press>().nPressID = nLineID;

                //プレス機の移動を許可
                bFirstMove = true;

                //リリースチェックをtrueにして線を引けないようにする
                bReleaseCheck = true;
            }

        }

        if (bSLineS == true)
        {
            _touch.bStickyS = true;
        }
        if (bSLineE == true)
        {
            _touch.bStickyE = true;
        }



    }

    public void Visible()
    {
        //lvPointStorage.Clear();
        lRendere.material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
    }

}


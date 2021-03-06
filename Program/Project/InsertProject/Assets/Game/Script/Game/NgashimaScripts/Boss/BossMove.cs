﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour 
{
    // ----- プライベート変数 -----
    private GameObject MainCamera;          // メインカメラ

    [SerializeField]
    private float fMoveSpeed = 0;
    [SerializeField]
    private float fFloatSpeed = 0;
    [SerializeField]
    private float fPosX = 0;
    [SerializeField]
    private float RotateSpeed = 0;
    [SerializeField]
    private int nMoveNum = 0;

    [SerializeField]
    private GameObject TeleportEffectObj;         // ワームホールオブジェクト

    private Vector3 BossStartPos;

    private bool bFront = false;
    private bool bFloat = false;

    private BossEmergency _BossEmergency;   // ボス出現スクリプト

    private float fMoveIntervalTime = 0.0f;     // 移動間隔時間

    // ===== 制御番号をゲットする関数 =====
    public int GetMoveNum()
    {
        return nMoveNum;
    }

    // ===== 制御番号をセットする関数 =====
    public void SetMoveNum(int num)
    {
        nMoveNum = num;
    }

	// ===== スタート関数 =====
	void Start () 
    {
        MainCamera = GameObject.Find("Main Camera");

        _BossEmergency = this.GetComponent<BossEmergency>();
        BossStartPos = new Vector3(0.0f, 0.0f, -0.1f);

		CSoundManager.Instance.StopBGM();
		CSoundManager.Instance.PlayBGM(AUDIO_LIST.BGM_BOSS);
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        if (_BossEmergency.GetBossAttack() == true && MainCamera.transform.localPosition.y <= _BossEmergency.GetCameraStartPos().y)
        {
            fMoveIntervalTime += Time.deltaTime;

            if (fMoveIntervalTime >= 5.0f)
            {
                Instantiate(TeleportEffectObj, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.3f, this.transform.localPosition.z - 1.7f), Quaternion.identity);

                this.transform.localPosition = new Vector3(Random.Range(-1.5f, 2.5f), Random.Range(-2, 5), 0);

                Instantiate(TeleportEffectObj, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.3f, this.transform.localPosition.z - 1.7f), Quaternion.identity);

                fMoveIntervalTime = 0.0f;
            }
            
            //// 上下に移動
            //this.transform.localPosition += new Vector3(0.0f, 0.0f, fFloatSpeed) * Time.deltaTime;

            //// 指定地点に到達したら反転させる
            //if (transform.localPosition.z >= BossStartPos.z && bFloat == false)
            //{
            //    bFloat = true;
            //    fFloatSpeed *= -1;
            //}
            //if (transform.localPosition.z <= BossStartPos.z - 0.2f && bFloat == true)
            //{
            //    bFloat = false;
            //    fFloatSpeed *= -1;
            //}


            //// 左右に移動
            //switch (nMoveNum)
            //{
            //    case 0: // 正面を向く（待機）
            //        //if (this.transform.localEulerAngles.y <= 180)
            //        //{
            //        //    if(!bFront)
            //        //        this.transform.Rotate(0, RotateSpeed, 0);
            //        //    if (this.transform.localEulerAngles.y >= 180)
            //        //        bFront = true;

            //        //}
            //        //else
            //        //{
            //        //    if(!bFront)
            //        //    this.transform.Rotate(0, -RotateSpeed, 0);
            //        //    if (this.transform.localEulerAngles.y <= 180)
            //        //        bFront = true;
            //        //}

            //        break;
            //    case 1: // 右に移動
            //        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
            //                                                      new Vector3(fPosX, this.transform.localPosition.y, this.transform.localPosition.z),
            //                                                      fMoveSpeed * Time.deltaTime);

            //        if (bFront)
            //            bFront = false;

            //        // 右に向く
            //        //if (this.transform.localEulerAngles.y >= 90)
            //        //    this.transform.Rotate(0, -RotateSpeed, 0);

            //        // 右に到達したら
            //        if (this.transform.localPosition.x >= fPosX)
            //            nMoveNum = 2;
            //        break;
            //    case 2: // 左に移動
            //        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
            //                                                      new Vector3(-fPosX, this.transform.localPosition.y, this.transform.localPosition.z),
            //                                                      fMoveSpeed * Time.deltaTime);

            //        if (bFront)
            //            bFront = false;

            //        // 左に向く
            //        //if (this.transform.localEulerAngles.y <= 270)
            //        //    this.transform.Rotate(0, RotateSpeed, 0);

            //        // 左に到達したら
            //        if (this.transform.localPosition.x <= -fPosX)
            //            nMoveNum = 1;
            //        break;
            //}
        }
	}
}

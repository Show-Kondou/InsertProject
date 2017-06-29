using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraMove : MonoBehaviour 
{
    // ----- プライベート変数 -----
    private GameObject MainCamera;          // メインカメラ
    private GameObject BossObj;             // ボスオブジェクト

    private bool bFinish = false;           // 終了フラグ

    private float fIntervalTime = 0.0f;     // インターバルタイム

    private Vector3 CameraStartPos;         // カメラスタート位置

    // ===== フィニッシュフラグをゲットする関数 =====
    public bool GetFinishFlg()
    {
        return bFinish;
    }

	// ===== スタート関数 =====
	void Start () 
    {
        MainCamera = GameObject.Find("Main Camera");

        BossObj = this.transform.FindChild("Boss").gameObject;
        CameraStartPos = MainCamera.transform.localPosition;
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        if (BossObj.GetComponent<BossDefeat>().GetBossCollapse() == true)
        {
            fIntervalTime += Time.deltaTime;

            if (fIntervalTime >= 1.5f)
            {
                // カメラを元に戻す
                if (MainCamera.transform.localPosition.y > CameraStartPos.y)
                {
                    Debug.Log("カメラ戻したいんですが？");
                    // カメラを元に戻す
                    MainCamera.transform.localPosition = Vector3.MoveTowards(MainCamera.transform.localPosition,
                                                                             CameraStartPos,
                                                                             6.0f * Time.deltaTime);
                }
                else
                {
                    Debug.Log("フィニッシュ");
                    bFinish = true;
                }
            }
        }		
	}
}

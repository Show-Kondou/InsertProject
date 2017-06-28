using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEmergency : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    private GameObject MainCamera;          // メインカメラ

    private GameObject DevilEffectObj;      // 魔法陣オブジェクト

    private bool bEmergency = true;         // 出現フラグ
    private bool bAttack = false;           // 攻撃開始フラグ
    private bool bDevilEffect = true;       // 魔法陣フラグ

    private float fEmergencyTime = 0.0f;    // 出現タイム

    private Vector3 CameraStartPos;         // カメラのスタート位置
    private Vector3 BossStartPos;           // ボスのスタート位置

    // ===== カメラのスタート位置をゲットする関数 =====
    public Vector3 GetCameraStartPos()
    {
        return CameraStartPos;
    }

    // ===== ボスの出現フラグをゲットする関数 =====
    public bool GetBossAttack()
    {
        return bAttack;
    }

    // ===== ボスの出現フラグをセットする関数 ======
    public void SetBossAttack(bool attack)
    {
        bAttack = attack;
    }

	// ===== スタート関数 =====
	void Start () 
    {
        MainCamera = GameObject.Find("Main Camera");

        // アニメーターを格納
        animator = GetComponent<Animator>();

        DevilEffectObj = GameObject.Find("BossSet(Clone)").transform.FindChild("DevilEffect").gameObject;

        CameraStartPos = MainCamera.transform.localPosition;

        BossStartPos = GameObject.Find("BossSet(Clone)").transform.localPosition;
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        if (bDevilEffect)
        {
            // カメラをボスに向けてアップ
            MainCamera.transform.localPosition = Vector3.MoveTowards(MainCamera.transform.localPosition,
                                                                      new Vector3(BossStartPos.x + 0.0f, BossStartPos.y - 2.5f, BossStartPos.z - 4.5f),
                                                                      6.0f * Time.deltaTime);
        }
        if (!bDevilEffect && bAttack)
        {
            // カメラを元に戻す
            MainCamera.transform.localPosition = Vector3.MoveTowards(MainCamera.transform.localPosition,
                                                                      CameraStartPos,
                                                                      12.0f * Time.deltaTime);
        }

        // カメラが移動し終わったら
        if (MainCamera.transform.localPosition.y >= BossStartPos.y - 2.5f)
        {
            if (bDevilEffect && DevilEffectObj)
            {
                DevilEffectObj.SetActive(true); // 魔法陣の出現

                bDevilEffect = false;
            }

            if (bEmergency)
            {
                if (!bAttack)
                {
                    fEmergencyTime += Time.deltaTime;

                    // 地面から出現
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                                                                      new Vector3(0.0f, 0.0f, 0.0f),
                                                                      1.0f * Time.deltaTime);

                    // ボスが地上に出現したら
                    if (this.transform.localPosition.z <= 0.0f && fEmergencyTime >= 4.5f)
                    {
                        animator.SetTrigger("BrainControl");    // 槍を振る（出現演出）

                        bEmergency = false;
                        fEmergencyTime = 0.0f;
                    }
                }
            }
            else
            {
                fEmergencyTime += Time.deltaTime;

                if (fEmergencyTime >= 2.0f && !bAttack)
                {
                    bAttack = true;
                    fEmergencyTime = 0.0f;

                    Destroy(DevilEffectObj);    // 魔法陣を消す
                }
            }
        }
	}
}

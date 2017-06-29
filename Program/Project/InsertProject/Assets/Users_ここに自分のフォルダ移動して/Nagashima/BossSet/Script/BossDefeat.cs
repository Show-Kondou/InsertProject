using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeat : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    private GameObject MainCamera;          // メインカメラ

    private bool bCollapse = false;         // 負けフラグ
    private bool bExplosion = true;         // 爆発フラグ

    private float fDefeatTime = 0.0f;       // 敗北演出時間

    private GameObject ParticleManagerObj;  // パーティクルマネージャー

    [SerializeField]
    private GameObject TestExplosion;       // テストの爆発

    // ===== ボス崩壊フラグをゲットする関数 =====
    public bool GetBossCollapse()
    {
        return bCollapse;
    }

	// ===== スタート関数 =====
	void Start () 
    {
        // アニメーターを格納
        animator = GetComponent<Animator>();

        MainCamera = GameObject.Find("Main Camera");

        ParticleManagerObj = GameObject.Find("Managers").transform.FindChild("ParticleManager").gameObject;
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        if (this.GetComponent<BossDamage>().GetBossHitPoint() <= 0)
        {
            Destroy(this.GetComponent<BossEmergency>());

            if (bExplosion)
            {
                // カメラをボスに向けてアップ
                MainCamera.transform.localPosition = Vector3.MoveTowards(MainCamera.transform.localPosition,
                                                                         new Vector3(this.transform.localPosition.x + 0.0f, this.transform.localPosition.y - 2.5f, this.transform.localPosition.z - 4.0f),
                                                                         6.0f * Time.deltaTime);
            }

            // カメラが移動し終わったら
            if (MainCamera.transform.localPosition.y >= this.transform.localPosition.y - 2.5f)
            {
                fDefeatTime += Time.deltaTime;

                animator.SetBool("Knockdown", true);    // 敗北モーション再生

                if (fDefeatTime >= 1.5f && bExplosion)
                {
                    Instantiate(TestExplosion, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1.5f, this.transform.localPosition.z - 2.0f), Quaternion.identity);
                    bExplosion = false;
                    bCollapse = true;
                    this.gameObject.SetActive(false);
                }
            }
        }
	}
}

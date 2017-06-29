using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    [SerializeField]
    private float fHitPoint;                // ボスのヒットポイント
    private int nDamageUP = 0;              // ダメージアップ

    private bool bSandwich = true;          // 挟むフラグ
    private float fInvincibleTime = 0.0f;    // 無敵時間

    private bool bSmallDamage = false;      // 小ダメージアニメーションのフラグ
    private bool bBigDamega = false;        // 大ダメージアニメーションのフラグ

    private float fDamageTime = 0.0f;       // ダメージアニメーションカウント

    // ===== ボスのHPをゲットする関数 =====
    public float GetBossHitPoint()
    {
        return fHitPoint;
    }

    // ===== ボスのHPをセットする関数 =====
    public void SetBossHitPoint(float hp)
    {
        fHitPoint = hp;
    }

    // ===== ダメージを受けた（挟まれた）時の関数 =====
    public void BossHitDamage()
    {
        if (bSandwich)
        {
            Debug.Log("ボス挟まってる？");

            CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_BOSS_HIT_0);

            // 待機状態だった場合は通常のダメージモーション
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
                bSmallDamage = true;
            // 攻撃状態だった場合はキャンセルモーション
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BrainControl") == true || animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == true)
                bBigDamega = true;

            // 動きを止める
            this.GetComponent<BossMove>().SetMoveNum(0);

            fHitPoint -= 1;
            //nHitPoint -= (1 * 小スライムが挟まれた数) + (3 * 大スライムが挟まれた数) + nDamageUP（味方スライムの挟まれた総数）;

            bSandwich = false;
        }
    }

	// ===== スタート関数 =====
	void Start () 
    {
        // アニメーターを格納
        animator = GetComponent<Animator>();
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        // ダメージモーション
        if(bSmallDamage)
            animator.SetTrigger("Damage");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") == true)
        {
            fDamageTime += Time.deltaTime;

            if (fDamageTime >= 2.0f)
            {
                fDamageTime = 0.0f;
                animator.SetTrigger("Damage");
            }
        }
        // キャンセルモーション
        if(bBigDamega)
            animator.SetTrigger("Cancel");

        if (!bSandwich)
        {
            fInvincibleTime += Time.deltaTime;

            if (fInvincibleTime >= 1.0f)
            {
                // ボスを動かす
                this.GetComponent<BossMove>().SetMoveNum(Random.Range(1, 3));

                bSandwich = true;
                bSmallDamage = false;
                bBigDamega = false;
                fDamageTime = 0.0f;
                fInvincibleTime = 0.0f;
            }
        }
	}
}

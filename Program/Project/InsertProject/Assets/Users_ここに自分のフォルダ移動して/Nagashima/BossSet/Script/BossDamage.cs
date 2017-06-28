using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    [SerializeField]
    private float fHitPoint;      // ボスのヒットポイント

    private int nDamageUP = 0;  // ダメージアップ

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
        Debug.Log("ボス挟まってる？");

		CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_BOSS_HIT_0);

        // 待機状態だった場合は通常のダメージモーション
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
            animator.SetTrigger("Damage");
        // 攻撃状態だった場合はキャンセルモーション
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("BrainControl") == true || animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == true)
            animator.SetTrigger("Cancel");

        
        //nHitPoint -= (1 * 小スライムが挟まれた数) + (3 * 大スライムが挟まれた数) + nDamageUP（味方スライムの挟まれた総数）;
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
	}
}

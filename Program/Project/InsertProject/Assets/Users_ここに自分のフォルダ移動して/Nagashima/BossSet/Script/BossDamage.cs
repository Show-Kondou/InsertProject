using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour 
{
    Animator animator;  // アニメーター

    // ----- プライベート変数 -----
    [SerializeField]
    private int nHitPoint;

    // ===== ダメージを受けた（挟まれた）時の関数 =====
    public void BossHitDamage()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true)
            animator.SetTrigger("Damage");

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("BrainControl") == true || animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") == true)
            animator.SetTrigger("Cancel");
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

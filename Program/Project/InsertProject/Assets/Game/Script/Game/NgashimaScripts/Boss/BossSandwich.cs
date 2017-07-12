using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSandwich : CSSandwichObject 
{
    // ===== スタート関数 =====
    void Start()
    {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
    }

    public override void Execute(float deltaTime)
    {
        base.Execute(deltaTime);
    }

    public override void LateExecute(float deltaTime)
    {
        base.LateExecute(deltaTime);
    }

    // ===== 挟まれた時 =====
    public override void SandwichedAction()
    {
		CSameSandwichAction.AddPressMachineList(m_HitIDA, m_HitIDB, true);
        // ボスにダメージを与える  
        this.transform.FindChild("Boss").gameObject.GetComponent<BossDamage>().BossHitDamage();
    }
}

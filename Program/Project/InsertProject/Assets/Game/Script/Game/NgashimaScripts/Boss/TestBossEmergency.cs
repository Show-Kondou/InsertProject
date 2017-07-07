using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossEmergency : CSSandwichObject
{

	// Use this for initialization
    void Start()
    {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
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
        this.transform.FindChild("Boss").gameObject.GetComponent<BossDamage>().BossHitDamage();
    }
}

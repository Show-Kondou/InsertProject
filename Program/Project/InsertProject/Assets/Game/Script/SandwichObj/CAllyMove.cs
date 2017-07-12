using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllyMove : CSlimeMove {

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_Moving = false;
		m_Position = transform.position;
		m_MoveTimer = m_WaitTime;
		m_Rotation = Random.Range(0, 360);
		transform.rotation = Quaternion.Euler(m_Rotation, -90, 90);
	}

	public override void Execute(float deltaTime) {
		Debug.Log("みかた");
		base.Execute(deltaTime);
	}

	public override void LateExecute(float deltaTime) {
		base.LateExecute(deltaTime);
	}

	/// <summary>
	/// 挟まれた時の処理
	/// </summary>
	public override void SandwichedAction() {
		if(m_Invincible)
			return;
		CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.AllySlimeDeath, transform.position);
		Destroy(gameObject);
	}
}

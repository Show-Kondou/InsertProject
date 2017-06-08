using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllyMove : CSSandwichObject {

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
		base.Execute(deltaTime);
		// ジャンプ中処理
		if(m_Moving) {
			m_Position.x += Mathf.Cos(m_Rotation - 180) * m_MoveSpped * deltaTime;
			m_Position.y += Mathf.Sin(m_Rotation - 180) * m_MoveSpped * deltaTime;
			m_Position.z = -VerticalThrowingUp(m_JumpTimer, m_JumpPower);   // 上移動
			if(VerticalThrowingUp(m_JumpTimer, m_JumpPower) < 0) {          // 地面にめり込んだら終わり
				m_Moving = false;
				m_Position.z = 0;
			}
			transform.position = m_Position;	// 変更したポジションで更新
			m_JumpTimer += deltaTime;
			transform.rotation = Quaternion.Euler(m_Rotation, -90, 90);
			return;
		}
		Debug.Log(m_MoveTimer);
		m_MoveTimer -= deltaTime;
		// 待ち時間が0になったらジャンプ
		if(m_MoveTimer < 0) {
			m_Moving = true;
			m_MoveTimer = m_WaitTime;
			m_Position = transform.position;
			m_JumpTimer = 0;
			m_Rotation = Random.Range(0, 360);
		}
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
		CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.EXPLOSION, transform.position);
		Destroy(gameObject);
	}
}

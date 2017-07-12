//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CEnemyMove.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	エネミースクリプト
//	
//==================================================
//	作成日：2017/06/07
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMove : CSlimeMove {
	[SerializeField]
	private GameObject AllyPrefab;	// 味方プレハブ

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
	}

	public override void LateExecute(float deltaTime) {
		base.LateExecute(deltaTime);

	}

	/// <summary>
	/// 挟まれた時の処理
	/// </summary>
	public override void SandwichedAction() {
		var obj = Instantiate(AllyPrefab, transform.position, Quaternion.identity);
		obj.GetComponent<CAllyMove>().m_Invincible = true;
		obj.GetComponent<CAllyMove>().m_InvincibleTimer = 2.0f;
		Destroy(gameObject);	// 役目を終えたので削除
	}
}
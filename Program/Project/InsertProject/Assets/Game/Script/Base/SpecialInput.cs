
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	SpecialInput.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	いろいろな入力系スクリプト(になる予定。現在ダブルタップのみ)
//	
//==================================================
//	作成日：2017/06/14
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInput : ObjectBase {
	static public bool m_bDoubleTap;		// ダブルタップ判定用
	static public Vector3 m_vDoubleTapPos;	// ダブルタップした場所
	private float m_DoubleTapTimer;         // ダブルタップ判定待ち時間
	[SerializeField]
	private float m_DoubleTapCheckTime;		// ダブルタップ判定時間

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_DoubleTapTimer = 0;
		m_bDoubleTap = false;
	}

	public override void Execute(float deltaTime) {
		DoubleTapCheck(deltaTime);
	}

	public override void LateExecute(float deltaTime) {
		m_bDoubleTap = false;
	}

	private void DoubleTapCheck(float deltaTime) {

		// ダブルタップ判定
		if(Input.GetMouseButtonDown(0)) {
			if(m_DoubleTapTimer <= 0) {
				m_DoubleTapTimer = m_DoubleTapCheckTime;
			} else {
				m_bDoubleTap = true;
				m_DoubleTapTimer = 0;
			}
		}
		m_DoubleTapTimer -= deltaTime;

	}
}

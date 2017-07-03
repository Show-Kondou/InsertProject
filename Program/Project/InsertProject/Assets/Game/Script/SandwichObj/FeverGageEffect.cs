
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	FeverGageEffect.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	フィーバーゲージチャージの移動を制御。(エルミート曲線)
//	
//==================================================
//	作成日：2017/06/24
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverGageEffect : ObjectBase {
	RectTransform rectTransform = null;
	private float m_Direction;    // 向き
	[SerializeField]
	private Vector3 m_MoveTarget;   // エフェクトの移動位置
	private float m_ElapsedTime;
	[SerializeField]
	private float m_TargetTime;
	private Vector3 FirstPos;
	[SerializeField]
	private GameObject m_FeverGage;
	// Use this for initialization
	void Start () {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_FeverGage = GameObject.Find("Fiver");
	}

	public void SetFirstPosition(Vector3 Position) {
		rectTransform = GetComponent<RectTransform>();
		rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Position);
		FirstPos = rectTransform.position;
		m_ElapsedTime = 0;
		if(Position.x < 0)
			m_Direction = -1.0f;
		else
			m_Direction = 1.0f;
	}

	public override void Execute(float deltaTime) {
		m_ElapsedTime += deltaTime;
		rectTransform.position = CSpecialCalculation.HermiteCurve(
			FirstPos,
			m_MoveTarget, 
			new Vector3(866.0f * m_Direction, 500.0f, 0),
			new Vector3(866.0f * -m_Direction, 500.0f, 0),
			m_ElapsedTime / m_TargetTime);
		if(m_ElapsedTime / m_TargetTime > 1.0f) {
			m_FeverGage.GetComponent<FiverGauge>().AddFiver(4.0f);
			ObjectManager.Instance.DeleteObject(m_OrderNumber, m_ObjectID);
		}
	}

	public override void LateExecute(float deltaTime) {

	}
}

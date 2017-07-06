
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CSameSandwichAction.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	同じプレス機に挟まれたかを検索し条件に基づき実行する
//	
//==================================================
//	作成日：2017/07/06
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSameSandwichAction : ObjectBase {
	public struct PressMachines{
		public int HitIDA;
		public int HitIDB;
		public bool BossPress;
		public float LifeTime;
	};

	static private List<PressMachines> pressList = new List<PressMachines>();
	static public int m_BossDamage; 

	// Use this for initialization
	void Start() {
		m_OrderNumber = 4;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {
		m_BossDamage = 0;

	}

	public override void LateExecute(float deltaTime) {
	}

	/// <summary>
	/// 5個以上もしくはボスを挟んだプレス機のIDを格納する
	/// </summary>
	/// <param name="First">最初のID</param>
	/// <param name="Second">2個目のID</param>
	/// <param name="bossPress">ボスを挟んだか否か</param>
	static public void AddPressMachineList(int First, int Second, bool bossPress = false) {
		foreach(PressMachines obj in pressList) {
			if(obj.HitIDA == First && obj.HitIDB == Second)
				return;
		}
		foreach(CSSandwichObject sandObj in CSSandwichObjManager.m_SandwichObjList) {
			foreach(CSSandwichObject.PressObject pressObj in sandObj.m_PressObjList) {
				if(First == pressObj.HitID || Second == pressObj.HitID)
					m_BossDamage++;
			}
		}
	}
}

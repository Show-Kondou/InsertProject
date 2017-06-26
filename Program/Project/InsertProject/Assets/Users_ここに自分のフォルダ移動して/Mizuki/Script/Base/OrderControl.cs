
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	OrderControl.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	更新順の管理をする
//	
//==================================================
//	作成日：2017/03/12
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderControl : MonoBehaviour {
	public int m_OrderNumber;       // 実行順
	[SerializeField]
	private List<ObjectBase> m_ObjList;

	// Use this for initialization
	void Start () {

	}

	/// <summary>
	/// 更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
	public void Execute(float deltaTime) {
		for(int i = 0; i < m_ObjList.Count; i++) {
			m_ObjList[i].Execute(deltaTime);
		}
	}

	/// <summary>
	/// 後更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
	public void LateExecute(float deltaTime) {
		for(int i = 0; i < m_ObjList.Count; i++) {
			m_ObjList[i].LateExecute(deltaTime);
		}
	}

	/// <summary>
	/// オブジェクトの追加
	/// </summary>
	/// <param name="newObj">追加オブジェクト</param>
	public void AddList(ObjectBase newObj) {
		int i;				// ループ用
		bool match = false;	// マッチフラグ
		// 使っていないIDの検索
		for(i = 0; i < m_ObjList.Count; i++, match = false) {
			foreach(ObjectBase Obj in m_ObjList) {
				if(Obj.ID == i) {
					match = true;
					break;
				}
			}
			if(!match) {
				newObj.ID = i;
				m_ObjList.Add(newObj);
				return;
			}
		}
		newObj.ID = i;
		m_ObjList.Add(newObj);
	}

	public bool DeleteObjectInOrderList(int deleteID) {
		foreach(ObjectBase obj in m_ObjList) {
			if(obj.ID == deleteID) {
				m_ObjList.Remove(obj);
				Destroy(obj.gameObject);
				return true;
			}
		}
		return false;
	}
}

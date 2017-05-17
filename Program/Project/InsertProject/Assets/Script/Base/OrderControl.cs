
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
		m_ObjList.Add(newObj);
	}
}

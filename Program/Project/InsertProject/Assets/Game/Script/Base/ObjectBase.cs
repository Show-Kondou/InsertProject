
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	ObjectBase.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	オブジェクトのベース
//
//	
//==================================================
//	作成日：2017/03/12
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour {
	protected int m_OrderNumber = 0;

	// Use this for initialization
	void Start () {
		Debug.Log("とおった");
        ObjectManager.Instance.RegistrationList(this,m_OrderNumber);
	}
	
	/// <summary>
	/// 更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
    public virtual void Execute(float deltaTime) {

    }

	/// <summary>
	/// 後更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
    public virtual void LateExecute(float deltaTime) {

    }

	/*
	public void ChangeOrderNumber(int OrderNumber) {

	}
	*/
}

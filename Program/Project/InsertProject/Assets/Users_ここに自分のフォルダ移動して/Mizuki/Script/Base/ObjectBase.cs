
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
	protected bool m_Using = true;
	protected int m_ObjectID;

	public int ID {
		get
		{
			return m_ObjectID;
		}
		set
		{
			m_ObjectID = value;
		}
	}

	// Use this for initialization
	void Start () {
        ObjectManager.Instance.RegistrationList(this,m_OrderNumber);
	}
	
	/// <summary>
	/// 更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
    public virtual void Execute(float deltaTime) {
		if(!m_Using)
			return;
    }

	/// <summary>
	/// 後更新
	/// </summary>
	/// <param name="deltaTime">デルタタイム</param>
    public virtual void LateExecute(float deltaTime) {

    }

	/// <summary>
	/// オブジェクトの削除
	/// </summary>
	/// <param name="OrderNumber"></param>
	public bool DeleteObject() {

		return false;
	}
	
}

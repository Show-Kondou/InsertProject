
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	ObjectManager.cs
//	
//	作成者:佐々木瑞生		
//==================================================
//	概要
//	全オブジェクトの管理、更新
//	
//==================================================
//	作成日：2017/03/12
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : SingletonMonoBehaviour<ObjectManager> {
	[SerializeField]
	private List<OrderControl> m_OrderList;
	[SerializeField]
	private OrderControl m_OrderControlPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;
        foreach(OrderControl orderControl in m_OrderList) {
            orderControl.Execute(deltaTime);
        }
        foreach(OrderControl orderControl in m_OrderList) {
            orderControl.LateExecute(deltaTime);
        }
	}

    /// <summary>
	/// オブジェクトの追加
	/// </summary>
	/// <param name="newObj">追加するオブジェクト</param>
	/// <param name="OrderNumber">オーダーの番号、デフォルトは0</param>
    public void RegistrationList(ObjectBase newObj, int OrderNumber = 0) {
		if(m_OrderList.Count <= OrderNumber) {
			for(int i = m_OrderList.Count; i <= OrderNumber; i++) {
				OrderControl tmp = Instantiate(m_OrderControlPrefab);
				m_OrderList.Add(tmp);
				tmp.transform.parent = transform;
			}
		}
		m_OrderList[OrderNumber].AddList(newObj);
    }
}

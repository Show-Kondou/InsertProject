//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*
//	CSSandwichObjManager.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	挟まれる子達の管理
//	
//==================================================
//	作成日：2017/05/19
//	
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSandwichObjManager : ObjectBase{
    public List<CSSandwichObject> m_SandwichObjList = new List<CSSandwichObject>();
    [SerializeField]
    private List<CSSandwichObject> m_ObjTypeList = new List<CSSandwichObject>();

    float m_Timer = 0;

    // Use this for initialization
    void Start() {
        m_OrderNumber = 1;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
        CreateSandwichObj(0, new Vector2(0, 0));	// テスト生成
    }

    public override void Execute(float deltaTime) {
		m_Timer += deltaTime;
		//if(m_Timer > 4.0f) {
		if(SpecialInput.m_bDoubleTap) { 
			m_Timer = 0;
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CreateSandwichObj(0, new Vector2(width, height));
		}
    }

    public override void LateExecute(float deltaTime) {

    }

    /// <summary>
    /// オブジェクトの生成
    /// </summary>
    /// <param name="objType">生成するオブジェクトの種類</param>
    /// <param name="Position">生成する座標</param>
    public void CreateSandwichObj(int objType, Vector2 Position) {
        CSSandwichObject obj = Instantiate(m_ObjTypeList[objType], Position, Quaternion.identity);
        m_SandwichObjList.Add(obj);
        obj.transform.parent = transform;	// 親子設定
    }

    public void DeleteSandwichObj() {

    }
}
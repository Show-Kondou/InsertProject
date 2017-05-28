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
        CreateSandwichObj(0, new Vector2(0, 0));
    }

    public override void Execute(float deltaTime) {

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
        obj.transform.parent = transform;
    }

    public void DeleteSandwichObj() {

    }
}
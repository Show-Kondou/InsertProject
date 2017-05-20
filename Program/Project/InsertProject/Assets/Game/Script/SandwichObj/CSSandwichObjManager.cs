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
    public List<CSSandwichObject> m_SandwichObjList;

    // Use this for initialization
    void Start() {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

    }

    public override void Execute(float deltaTime) {

    }

    public override void LateExecute(float deltaTime) {

    }
}
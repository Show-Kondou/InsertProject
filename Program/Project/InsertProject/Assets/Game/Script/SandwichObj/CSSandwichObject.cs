//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CSSandwichObject.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	挟まれるオブジェクトスクリプト
//	
//==================================================
//	作成日：2017/05/19
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSSandwichObject : ObjectBase {
    private bool m_bHitFlagA;   // 一個目に当たったかのチェック
    private bool m_bHitFlagB;   // 二個目に当たったかのチェック
    public bool m_bInsert;      // 挟まりました

    // Use this for initialization
    void Start() {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

    }

    public override void Execute(float deltaTime) {

    }

    public override void LateExecute(float deltaTime) {

    }

    /// <summary>
    /// ヒットチェック
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay(Collider col) {
        if(col.tag != "" && col.tag != "") {
            return;
        }

        if(col.tag == "") {
            if(m_bHitFlagB)
                m_bInsert = true;
            else
                m_bHitFlagA = true;
        }

        if(col.tag == "") {
            if(m_bHitFlagA)
                m_bInsert = true;
            else
                m_bHitFlagB = true;
        }

    }
}
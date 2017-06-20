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
    public static List<CSSandwichObject> m_SandwichObjList = new List<CSSandwichObject>();
    [SerializeField]
    private List<CSSandwichObject> m_ObjTypeList = new List<CSSandwichObject>();

    float m_Timer = 0;

	private static int TotalMakeObj;	// オブジェクトを生成した数(ID用)

    // Use this for initialization
    void Start() {
        m_OrderNumber = 1;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
        CreateSandwichObj(0, new Vector2(0, 0));    // テスト生成
		TotalMakeObj = 0;
    }

    public override void Execute(float deltaTime) {
		base.Execute(deltaTime);
		m_Timer += deltaTime;
        //if (m_Timer > 4.0f){
        if (SpecialInput.m_bDoubleTap)
        {
            m_Timer = 0;
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CreateSandwichObj(0, new Vector2(width, height));
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CreateSandwichObj(2, new Vector2(width, height));
		}
    }

    public override void LateExecute(float deltaTime) {

    }

    /// <summary>
    /// オブジェクトの生成
    /// </summary>
    /// <param name="objType">生成するオブジェクトの種類</param>
    /// <param name="Position">生成する座標</param>
    public CSSandwichObject CreateSandwichObj(int objType, Vector2 Position) {
        CSSandwichObject obj = Instantiate(m_ObjTypeList[objType], Position, Quaternion.identity);
        m_SandwichObjList.Add(obj);
        obj.transform.parent = transform;   // 親子設定
		obj.m_ObjectID = TotalMakeObj;
		TotalMakeObj++;		// 次のオブジェクト用に数値をプラス
		return obj;
    }

	/// <summary>
	/// 指定IDのオブジェクトを削除
	/// </summary>
	/// <param name="ID">削除するオブジェクトのID</param>
    static public void DeleteSandwichObjToList(int ID) {
		for(int i = 0; i < m_SandwichObjList.Count; i++) {
			if(m_SandwichObjList[i].m_ObjectID == ID) {
				m_SandwichObjList.RemoveAt(i);
				break;
			}
		}
    }

	/// <summary>
	/// フィーバースライムの情報を返す。
	/// 敵スライムはフィーバースライムに吸い寄せられる。
	/// </summary>
	/// <returns></returns>
	static public CSlimeMove GetFeverSilmeData() {
		foreach(CSlimeMove objData in m_SandwichObjList) {
			if(objData.myType == CSlimeMove.SLIME_TYPE.Fever) {
				return objData;
			}
		}
		return null;
	}
}
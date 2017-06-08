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
	private float GravityValue = 9.8f;  // 重力加速度

	[SerializeField]
	float m_PressRangeLow;			// 挟まれ判定をする角度の下限。
	public float m_WaitTime;		// 移動待機時間
	[SerializeField]
	protected float m_MoveSpped;	// 移動速度
	protected float m_MoveTimer;	// 移動待機時間タイマー
	protected bool m_Moving;		// 移動中か否か
	protected Vector3 m_Position;	// ポジション
	[SerializeField]
	protected float m_JumpPower;    // ジャンプ力
	protected float m_JumpTimer;    // 投げ上げ用時間
	protected float m_Rotation;		// 移動方向

	// プレス機のステータス格納用
	public struct PressObject { 
        public bool bHitFlagA;   // 一個目に当たったかのチェック
        public bool bHitFlagB;   // 二個目に当たったかのチェック
        public int  HitID;       // 当たったオブジェクトの番号
        public Vector3 DirectionVec;   // 進行方向ベクトル
        public string HitObjName;  // 名前(前後確認)
    };

    private List<PressObject> m_PressObjList = new List<PressObject>();  // プレス機のリスト
    public bool m_bInsert;      // 挟まりました

    float life = 5.0f;

    // Use this for initialization
    void Start() {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

    }

    public override void Execute(float deltaTime) {
		// 挟まれチェック
		if(m_PressObjList.Count < 2) {
			return;
		}
		for(int i = 0; i < m_PressObjList.Count - 1; ++i) {
			for(int j = i + 1; j < m_PressObjList.Count; ++j) {
				// 挟まれ判定
				Debug.Log(Mathematics.VectorRange(m_PressObjList[i].DirectionVec, m_PressObjList[j].DirectionVec));
				if((m_PressObjList[i].bHitFlagA && m_PressObjList[j].bHitFlagA && m_PressObjList[i].HitID == m_PressObjList[j].HitID) ||
					m_PressObjList[i].bHitFlagB && m_PressObjList[j].bHitFlagB && m_PressObjList[i].HitID == m_PressObjList[j].HitID) {
					continue;
				}
				if(Mathematics.VectorRange(m_PressObjList[i].DirectionVec, m_PressObjList[j].DirectionVec) > m_PressRangeLow) {
					CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.EXPLOSION, transform.position);
					Destroy(gameObject);
					break;
				}
			}
		}
		m_PressObjList.Clear(); // 毎フレームリセット。重そうなので別案考え中。
	}

    public override void LateExecute(float deltaTime) {
		m_PressObjList.Clear(); // 毎フレームリセット。重そうなので別案考え中。
	}

	/// <summary>
	/// 挟まれチェック
	/// </summary>
	/// <param name="col">ぶつかったオブジェクト</param>
	public void OnTriggerStay(Collider col) {
		string colName = col.name;
		colName = col.name;
		if(!colName.Contains("startPress") && !colName.Contains("endPress")) {  // 名前でプレス機か判定
			return;
		}
		Press pressObj = col.GetComponent<Press>();
		PressObject hitCheck = new PressObject();
		hitCheck.HitID = pressObj.nPressID;         // プレス機ID
		hitCheck.DirectionVec = pressObj.vLookPos;  // 前方ベクトル
		hitCheck.HitObjName = colName;              // 名前 
		if(colName.Contains("startPress")) {        // ヒットしたのが上か下かを判定
			hitCheck.bHitFlagA = true;
		}
		if(colName.Contains("endPress")) {
			hitCheck.bHitFlagB = true;
		}
		m_PressObjList.Add(hitCheck);               // 当たったプレス機リストに追加
	}

	/// <summary>
	/// 挟まれチェック2D
	/// </summary>
	/// <param name="col">ぶつかったオブジェクト</param>
	public void OnTriggerStay2D(Collider2D col) {
		string colName = col.name;
		colName = col.name;
		if(!colName.Contains("startPress") && !colName.Contains("endPress")) {  // 名前でプレス機か判定
			return;
		}
		Press pressObj = col.GetComponent<Press>();
		PressObject hitCheck = new PressObject();
		hitCheck.HitID = pressObj.nPressID;         // プレス機ID
		hitCheck.DirectionVec = pressObj.vLookPos;  // 前方ベクトル
		hitCheck.HitObjName = colName;              // 名前 
		if(colName.Contains("startPress")) {        // ヒットしたのが上か下かを判定
			hitCheck.bHitFlagA = true;
		}
		if(colName.Contains("endPress")) {
			hitCheck.bHitFlagB = true;
		}
		m_PressObjList.Add(hitCheck);               // 当たったプレス機リストに追加
	}

	protected float VerticalThrowingUp(float time, float firstSpeed) {
		float height = firstSpeed * time - 0.5f * GravityValue * Mathf.Pow(time,2);

		return height;
	}
}
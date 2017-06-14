using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlimeMove : CSSandwichObject {
	public enum SLIME_TYPE {
		Ally,
		Enemy,

		MAX_SLIME_TYPE,
	};

	SLIME_TYPE myType;
	[SerializeField]
	private GameObject SlimeMesh;
	[SerializeField]
	private Material EnemyMat;
	[SerializeField]
	private Material AllyMat;
	static public int EnemyNum = 0;	// 敵の総数

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_Moving = false;
		m_Position = transform.position;							// 初期位置に移動
		m_MoveTimer = m_WaitTime;									// 移動待ち時間
		m_Rotation = Random.Range(0, 360);                          // 初期向きをランダムで決定
		transform.rotation = Quaternion.Euler(m_Rotation, -90, 90);	// 向きを設定
		// マテリアルとタイプを設定
		if(transform.tag == "Ally") {
			myType = SLIME_TYPE.Ally;
			SlimeMesh.GetComponent<Renderer>().material = AllyMat;
		} else {
			EnemyNum++;
			myType = SLIME_TYPE.Enemy;
			SlimeMesh.GetComponent<Renderer>().material = EnemyMat;
			Debug.Log(EnemyNum);
		}
	}

	public override void Execute(float deltaTime) {
		base.Execute(deltaTime);

		//=============
		if(!this)
			return; // バグ回避用。要修正
		//=============

		// ジャンプ中処理
		if(m_Moving) {
			m_Position.x += Mathf.Cos(m_Rotation - 180) * m_MoveSpped * deltaTime;
			m_Position.y += Mathf.Sin(m_Rotation - 180) * m_MoveSpped * deltaTime;
			m_Position.z = -VerticalThrowingUp(m_JumpTimer, m_JumpPower);   // 上移動
			if(VerticalThrowingUp(m_JumpTimer, m_JumpPower) < 0) {          // 地面にめり込んだら終わり
				m_Moving = false;
				m_Position.z = 0;
				m_Rotation = Random.Range(0, 360);  // 向きをランダムで決める
				transform.rotation = Quaternion.Euler(m_Rotation, -90, 90);
			}
			transform.position = m_Position;    // 変更したポジションで更新
			m_JumpTimer += deltaTime;
			return;
		}
		m_MoveTimer -= deltaTime;
		// 待ち時間が0になったらジャンプ
		if(m_MoveTimer < 0) {
			m_Moving = true;                    // 移動中フラグオン
			m_MoveTimer = m_WaitTime;           // 待ち時間再入
			m_Position = transform.position;    // 位置更新
			m_JumpTimer = 0;                    // ジャンプ経過時間タイマーをリセット
		}
	}

	public override void LateExecute(float deltaTime) {
		base.LateExecute(deltaTime);

	}

	/// <summary>
	/// 挟まれた時の処理
	/// </summary>
	public override void SandwichedAction() {
		if(myType == SLIME_TYPE.Enemy) {
			transform.tag = "Ally";			// タグを味方に
			m_Invincible = true;			// 無敵オン
			m_InvincibleTimer = 1.0f;		// 無敵時間
			SlimeMesh.GetComponent<Renderer>().material = AllyMat;	// マテリアル変更
			myType = SLIME_TYPE.Ally;   // 属性を味方に
			m_PressObjList.Clear();
			EnemyNum--;
			Debug.Log(EnemyNum);
		} else if(myType == SLIME_TYPE.Ally) {
			// パーティクルを出して削除
			CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.EXPLOSION, transform.position);
			Destroy(gameObject);
		}

	}
}

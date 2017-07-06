﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovieSlimeMove : CSSandwichObject {
	//public enum SLIME_TYPE {
	//	Ally,
	//	Enemy,
	//	Fever,
	//	Nothing,
	//	Big,

	//	MAX_SLIME_TYPE,
	//};

	//public SLIME_TYPE myType;
	[SerializeField]
	private GameObject SlimeMesh;   // スライムのモデル
	[SerializeField]
	private Material EnemyMat;      // 敵マテリアル
	[SerializeField]
	private Material AllyMat;       // 味方マテリアル
	[SerializeField]
	private Material FeverMat;      // フィーバーマテリアル
	[SerializeField]
	private GameObject FlagObj;     // 旗モデル
	static public int EnemyNum = 0; // 敵の総数
	[SerializeField]
	private float NothingLifeTime = 1.0f;   // 設定時間
	[SerializeField]
	private CFeverSlimeMovePointManager pointMng;   // フィーバースライム目的地
	private float NothingLifeTimer; // 3段階目になった時に時間で死亡させる。
	[SerializeField]
	private ParticleSystem m_part;
	[SerializeField]
	private FeverGageEffect m_FeverGageEffect;
	[SerializeField]
	private GameObject m_EffectCanvas;
	// Use this for initialization
	void Start() {
		FlagObj = new GameObject();
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_Moving = false;
		m_Position = transform.position;                            // 初期位置に移動
		m_MoveTimer = m_WaitTime;                                   // 移動待ち時間
																	//m_Rotation = Random.Range(0, 360 * Mathf.PI / 180);         // 初期向きをランダムで決定
		m_Rotation = Random.Range(0, 360);  // 向きをランダムで決める

		transform.rotation = Quaternion.Euler(0, 0, m_Rotation);    // 向きを設定
		FlagObj.SetActive(false);
		// 生成されたスライムごとにマテリアルと自身のタイプを設定、初期化
		if(transform.tag == "Enemy") {
			EnemyNum++;
			myType = SLIME_TYPE.Enemy;
			SlimeMesh.GetComponent<Renderer>().material = EnemyMat;
		} else if(transform.tag == "Ally") {
			myType = SLIME_TYPE.Ally;
			SlimeMesh.GetComponent<Renderer>().material = AllyMat;
		} else if(transform.tag == "Fever") {
			myType = SLIME_TYPE.Fever;
			SlimeMesh.GetComponent<Renderer>().material = FeverMat;
			FlagObj.SetActive(true);
			pointMng = GameObject.Find("FeverSlimeMovePoints").GetComponent<CFeverSlimeMovePointManager>();
			pointMng.ChangeNextPosition(0); // 初期目的地設定
			transform.position = pointMng.FeverSlimeDestination;    // 初期目的地移動
		} else {
			myType = SLIME_TYPE.Big;
			SlimeMesh.GetComponent<Renderer>().material = AllyMat;
		}
		NothingLifeTimer = NothingLifeTime;
		m_EffectCanvas = GameObject.Find("UI");
	}

	public override void Execute(float deltaTime) {
		base.Execute(deltaTime);

		Cursor.visible = false;
		//=============
		if(!this)
			return; // バグ回避用。要修正
		//=============

		// 一定時間残留後、消去
		if(myType == SLIME_TYPE.Nothing) {
			NothingLifeTimer -= deltaTime;
			if(NothingLifeTimer < 0) {
				CSSandwichObjManager.Instance.DeleteSandwichObjToList(m_SandwichObjectID);	// サンドイッチリストから削除
				ObjectManager.Instance.DeleteObject(m_OrderNumber, m_ObjectID);             // オブジェクトリストから削除
			}
			return;
		}

		// ジャンプ中処理
		if(m_Moving) {
			m_Position.x += Mathf.Cos(m_Rotation) * m_MoveSpped * deltaTime;// 横移動
			m_Position.y += Mathf.Sin(m_Rotation) * m_MoveSpped * deltaTime;// 縦移動
			m_Position.z = -VerticalThrowingUp(m_JumpTimer, m_JumpPower);   // 上移動
			if(VerticalThrowingUp(m_JumpTimer, m_JumpPower) < 0) {          // 地面にめり込んだら終わり
				m_Moving = false;
				m_Position.z = 0;
			}
			transform.position = m_Position;    // 変更したポジションで更新
			m_JumpTimer += deltaTime;
			return;
		}
		m_MoveTimer -= deltaTime;
		// 待ち時間が0になったらジャンプ
		if(m_MoveTimer < 0) {
			CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_SLIME_CONVERT);
			m_Moving = true;                    // 移動中フラグオン
			m_MoveTimer = m_WaitTime;           // 待ち時間再入
			m_Position = transform.position;    // 位置更新
			m_JumpTimer = 0;                    // ジャンプ経過時間タイマーをリセット
												// 自身のタイプごとに動作を変更
			switch(myType) {
				case SLIME_TYPE.Big:
				case SLIME_TYPE.Ally:
					m_Rotation = Random.Range(0, 360 * Mathf.PI / 180);  // 向きをランダムで決める
					break;
				case SLIME_TYPE.Enemy:
					var container = CSSandwichObjManager.Instance.GetFeverSilmeData();
					if(container) {
						m_Rotation = Mathf.Atan2(container.transform.position.y - transform.position.y,
							container.transform.position.x - transform.position.x); // フィーバースライムに向かう
					} else {
						m_Rotation = Random.Range(0, 360 * Mathf.PI / 180);  // 向きをランダムで決める
					}
					break;
				case SLIME_TYPE.Fever:
					// 外周移動
					Debug.Log(pointMng.FeverSlimeDestination);
					var containerFever = pointMng.FeverSlimeDestination;
					m_Rotation = Mathf.Atan2(containerFever.y - transform.position.y,
							containerFever.x - transform.position.x);
					break;
				default:
					break;
			}
			transform.rotation = Quaternion.Euler(0, 0, m_Rotation * Mathf.Rad2Deg - 180);  // 向きを変える
		}
	}

	public override void LateExecute(float deltaTime) {
		base.LateExecute(deltaTime);
	}

	/// <summary>
	/// 挟まれた時の処理
	/// </summary>
	public override void SandwichedAction() {
		gameObject.transform.parent = null;
		if(myType == SLIME_TYPE.Enemy) {
			SameTimeSandObjNum();
			m_Invincible = true;            // 無敵オン
			m_InvincibleTimer = 1.0f;       // 無敵時間
			CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.AllySlimeDeath, transform.position);
			ChangeSlimeState(SLIME_TYPE.Ally);
		} else if(myType == SLIME_TYPE.Ally) {
			SameTimeSandObjNum();
			myType = SLIME_TYPE.Nothing;   // 属性を味方に
			var obj = Instantiate(m_FeverGageEffect);
			obj.transform.parent = m_EffectCanvas.transform;
			obj.SetFirstPosition(transform.position);
			m_Invincible = true;            // 無敵オン
			m_InvincibleTimer = 1.0f;       // 無敵時間
			SlimeMesh.SetActive(false);
			// パーティクルを出す
			CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.AllySlimeDeath, transform.position);
		} else if(myType == SLIME_TYPE.Nothing) {
			SameTimeSandObjNum();
		} else if(myType == SLIME_TYPE.Big) {

		}

	}

	public void ChangeSlimeState(SLIME_TYPE newType) {
		switch(newType) {
			case SLIME_TYPE.Enemy:
				transform.tag = "Enemy";        // タグを敵に
				m_Invincible = true;            // 無敵オン
				m_InvincibleTimer = 1.0f;       // 無敵時間
				SlimeMesh.GetComponent<Renderer>().material = EnemyMat;  // マテリアル変更
				myType = SLIME_TYPE.Enemy;      // 属性を敵に
				m_PressObjList.Clear();
				EnemyNum++;
				break;
			case SLIME_TYPE.Ally:
				transform.tag = "Ally";         // タグを味方に
				m_Invincible = true;            // 無敵オン
				m_InvincibleTimer = 1.0f;       // 無敵時間
				SlimeMesh.GetComponent<Renderer>().material = AllyMat;  // マテリアル変更
				myType = SLIME_TYPE.Ally;   // 属性を味方に
				m_PressObjList.Clear();
				EnemyNum--;
				break;
			default:
				break;
		}
	}
}
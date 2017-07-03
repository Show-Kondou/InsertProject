
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CSlimeMove.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	スライムの移動制御
//  本当はコイツを継承させたかったけどうまくいかなかったのでswitchに変更。
//	
//==================================================
//	作成日：2017/06/12
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlimeMove : CSSandwichObject {
	[SerializeField]
	private GameObject SlimeMesh;	// スライムのモデル
	[SerializeField]
	private Material EnemyMat;		// 敵マテリアル
	[SerializeField]
	private Material AllyMat;		// 味方マテリアル
	[SerializeField]
	private Material FeverMat;		// フィーバーマテリアル
	[SerializeField]
	private GameObject FlagObj;		// 旗モデル
	static public int EnemyNum = 0; // 敵の総数
	[SerializeField]
	private float NothingLifeTime = 1.0f;   // 設定時間
	[SerializeField]
	private CFeverSlimeMovePointManager pointMng;	// フィーバースライム目的地
	private float NothingLifeTimer; // 3段階目になった時に時間で死亡させる。
	[SerializeField]
	private ParticleSystem m_part;
	[SerializeField]
	private FeverGageEffect m_FeverGageEffect;
	[SerializeField]
	private GameObject m_EffectCanvas;
	private bool m_OutZone;     // 画面外か否か
	public bool m_Sticky;		// ねばねばフラグ
	// Use this for initialization
	void Start() {
		FlagObj = new GameObject();
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_Moving = false;
		m_Position = transform.position;							// 初期位置に移動
		m_MoveTimer = m_WaitTime;                                   // 移動待ち時間
		//m_Rotation = Random.Range(0, 360 * Mathf.PI / 180);         // 初期向きをランダムで決定
		m_Rotation = 90.0f;         // 初期向きをランダムで決定
		transform.rotation = Quaternion.Euler(0, 0, m_Rotation);	// 向きを設定
		FlagObj.SetActive(false);
		// 生成されたスライムごとにマテリアルと自身のタイプを設定、初期化
		if(transform.tag == "Enemy") {
			EnemyNum++;
			myType = SLIME_TYPE.Enemy;
			SlimeMesh.GetComponent<Renderer>().material = EnemyMat;
		} else if(transform.tag == "Ally") {
			myType = SLIME_TYPE.Ally;
			SlimeMesh.GetComponent<Renderer>().material = AllyMat;
		} else if(transform.tag == "Fever"){
			myType = SLIME_TYPE.Fever;
			SlimeMesh.GetComponent<Renderer>().material = FeverMat;
			FlagObj.SetActive(true);
			pointMng = GameObject.Find("FeverSlimeMovePoints").GetComponent<CFeverSlimeMovePointManager>();
			pointMng.ChangeNextPosition(0);	// 初期目的地設定
			transform.position = pointMng.FeverSlimeDestination;    // 初期目的地移動
		}else {
			myType = SLIME_TYPE.Big;
			m_Invincible = true;		// 生成時に無敵を付与
			m_InvincibleTimer = 2.0f;   // 同上
			GetComponent<CircleCollider2D>().enabled = false;
		}
		NothingLifeTimer = NothingLifeTime;
		m_EffectCanvas = GameObject.Find("EffectCanvas");	// エフェクト用カンバスを取得
	}

	public override void Execute(float deltaTime) {
		base.Execute(deltaTime);
		//=============
		if(!this)
			return; // バグ回避用。要修正
		//=============

		if(myType == SLIME_TYPE.Nothing) {
			NothingLifeTimer -= deltaTime;
			if(NothingLifeTimer < 0) {
				CSSandwichObjManager.Instance.DeleteSandwichObjToList(m_SandwichObjectID);
				ObjectManager.Instance.DeleteObject(m_OrderNumber, m_ObjectID);             // オブジェクトリストから削除
			}
			return;
		}

		if(m_Sticky) {
			return;
		}

		// ジャンプ中処理
		if(m_Moving) {
			m_Position.x += Mathf.Cos(m_Rotation) * m_MoveSpped * deltaTime;
			m_Position.y += Mathf.Sin(m_Rotation) * m_MoveSpped * deltaTime;
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
			CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_SLIME_JUMP);
			m_Moving = true;                    // 移動中フラグオン
			m_MoveTimer = m_WaitTime;           // 待ち時間再入
			m_Position = transform.position;    // 位置更新
			m_JumpTimer = 0;                    // ジャンプ経過時間タイマーをリセット
			// 自身のタイプごとに動作を変更
			switch(myType) {
				case SLIME_TYPE.Big:
				case SLIME_TYPE.Ally:
					if(m_OutZone) {
						m_Rotation = Mathf.Atan2(-transform.position.y, -transform.position.x);
						break;
					}
					m_Rotation = Random.Range(0, 360 * Mathf.PI / 180);  // 向きをランダムで決める
					break;
				case SLIME_TYPE.Enemy:
					if(m_OutZone) {
						m_Rotation = Mathf.Atan2(-transform.position.y, -transform.position.x);
						break;
					}
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
			transform.rotation = Quaternion.Euler(0, 0, m_Rotation * Mathf.Rad2Deg - 180);	// 向きを変える
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
				myType = SLIME_TYPE.Enemy;		// 属性を敵に
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
				m_Sticky = false;
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// 画面外処理
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag != "OutZone") {
			return;
		}
		m_OutZone = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.tag != "OutZone") {
			return;
		}
		m_OutZone = false;
	}
}

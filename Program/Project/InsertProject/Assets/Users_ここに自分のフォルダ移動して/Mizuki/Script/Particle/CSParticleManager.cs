//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	CSParticleManager.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	particleの管理をする
//	
//==================================================
//	作成日：2017/05/14
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSParticleManager : SingletonMonoBehaviour<CSParticleManager> {
	// パーティクルリスト
	public enum PARTICLE_TYPE {
		AllySlimeEmarg = 0,		//  0:味方スライム出現
		BigAllySlimeEmarg,		//  1:でかスライム出現
		EnemySlimeEmarg,		//  2:敵スライム出現
		MagicWallEmarg,			//  3:マジックウォール出現
		MagicWallLine,			//  4:マジックウォールのライン用
		MagicWallUnion,			//  5:マジックウォールがぶつかった時
		FeverSlimeEmarg,		//  6:フィーバースライム出現
		FeverSlimeMove,			//  7:フィーバースライムの移動
		AllySlimeDeath,			//  8:味方スライム死亡
		BossEmarg,				//  9:ボス出現
		SlimeAttack,			// 10:スライム攻撃
		BrainWashAttack,		// 11:洗脳攻撃
		BossDefeat,				// 12:ボス撃破時

		MAX_PARTICLE_TYPE,		// 総パーティクル数
	};

    private List<CSParticleObject> m_ParticleList;		// 名前の通りですよ
    [SerializeField]
	private List<CSParticleObject> m_ParticleTypeList;  // パーティクルの種類を格納。ここから生成。
	[SerializeField]
	// 同じ種類ごとの数。Number0がn個、Number1がm個といった感じ。
	private int[] m_ParticleNumberByType;

	// Use this for initialization
	void Start() {
		m_ParticleList = new List<CSParticleObject>();
		m_ParticleNumberByType = new int[(int)PARTICLE_TYPE.MAX_PARTICLE_TYPE];
		for(int i = 0; i < (int)PARTICLE_TYPE.MAX_PARTICLE_TYPE; ++i) {
			CSParticleObject obj = Instantiate(m_ParticleTypeList[i]);
            m_ParticleNumberByType[i] = 0;
			obj.Create((PARTICLE_TYPE)i, 0);
		}

	}

    /// <summary>
    /// リストへ追加
    /// </summary>
    /// <param name="particle">パーティクルオブジェクト</param>
    /// <returns>リストの番号</returns>
    public int AddListParticle(CSParticleObject particle) {
        m_ParticleList.Add(particle);
        return m_ParticleList.Count;
    }

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    /// <param name="name">再生させるパーティクル</param>
    /// <returns>成功か否か(そのparticleが存在したかどうか)</returns>
    public CSParticleObject Play(PARTICLE_TYPE type, Vector3 position) {
		int nSameType = 0;
		for(int i = 0; i < m_ParticleList.Count; i++) {
			if(m_ParticleList[i].m_ParticleType == type/* && i == nSameType*/) {
				// 使用中でなければそのまま再生
				if(!m_ParticleList[i].m_Particle.isPlaying) {
					m_ParticleList[i].m_Particle.transform.position = position;
					m_ParticleList[i].m_Particle.Play();
					return m_ParticleList[i];	// ループから抜ける
				}
				// 使用中だったら
				nSameType++;	// 検索番号を増やす
				// まだ検索していないものがある場合再検索
				if(nSameType < m_ParticleNumberByType[(int)type]) {
					i = 0;	// 頭から再検索
				} else {
					// もうない場合生成する
					CSParticleObject obj = Instantiate(m_ParticleTypeList[(int)type],position,Quaternion.identity);
					m_ParticleNumberByType[(int)type]++;
					obj.Create(type, nSameType);
					obj.m_Particle.Play();
					return obj;
				}
			}
		}
        return null;
    }

	/// <summary>
	/// パーティクルの停止
	/// </summary>
	/// <param name="type">停止させるパーティクルの種類</param>
	/// <param name="sameNumber">停止させるパーティクルの種類</param>
	/// <returns>成功か否か(そのパーティクルが存在したかどうか)</returns>
	public bool Stop(PARTICLE_TYPE type, int sameNumber = 0) {
        for(int i = 0; i < m_ParticleList.Count; i++) {
            // タイプが一緒かつ番号が同じなら停止、真を返す
            if(m_ParticleList[i].m_ParticleType == type &&
               m_ParticleList[i].m_nNumberOfSameType != sameNumber) {
                m_ParticleList[i].m_Particle.Stop();
                return true;
            }
        }
        return false;   // 存在しなかったら偽を返す
    }
}

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
	public enum PARTICLE_TYPE {
		//EXPLOSION,

		MAX_PARTICLE_TYPE,
	};

    private List<CSParticleObject> m_ParticleList;		// 名前の通りですよ
    [SerializeField]
	private List<CSParticleObject> m_ParticleTypeList;	// パーティクルの種類を格納。ここから生成。
	private List<int> m_ParticleNumberByType;			// 同じ種類ごとの数。Number0がn個、Number1がm個といった感じ。
    // Use this for initialization
    void Start() {
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
    public CSParticleObject Play(PARTICLE_TYPE type) {
		int nSameType = 0;
        for(int i = 0; i < m_ParticleList.Count; i++) {
            if(m_ParticleList[i].name == name && i == nSameType) {
				if(!m_ParticleList[i].m_Particle.isPlaying) {	// 使用中でなければそのまま再生
					m_ParticleList[i].m_Particle.Play();
					return m_ParticleList[i];
				}else {                                         // 使用中だったら
					nSameType++;
					if(nSameType < m_ParticleNumberByType[(int)type]) {
						i = 0;
					}else {
						CSParticleObject obj = Instantiate(m_ParticleTypeList[(int)type]);
						obj.Create(type, nSameType);
					}
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
            if(m_ParticleList[i].name == name) {
                m_ParticleList[i].m_Particle.Stop();
                return true;
            }
        }
        return false;
    }
}

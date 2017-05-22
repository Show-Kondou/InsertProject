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
    [SerializeField]
    private List<CSParticleObject> m_ParticleList;   // 名前の通りですよ

    // Use this for initialization
    void Start() {
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
    public bool Play(string name) {
        for(int i = 0; i < m_ParticleList.Count; i++) {
            if(m_ParticleList[i].name == name) {
                m_ParticleList[i].m_Particle.Play();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// パーティクルの停止
    /// </summary>
    /// <param name="name">停止させるパーティクル</param>
    /// <returns>成功か否か(そのパーティクルが存在したかどうか)</returns>
    public bool Stop(string name) {
        for(int i = 0; i < m_ParticleList.Count; i++) {
            if(m_ParticleList[i].name == name) {
                m_ParticleList[i].m_Particle.Play();
                return true;
            }
        }
        return false;
    }
}

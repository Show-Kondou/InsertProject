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

    public int AddListParticle(CSParticleObject particle) {
        m_ParticleList.Add(particle);
        return m_ParticleList.Count;
    }

    public void Play(string name) {
        for(int i = 0; i < m_ParticleList.Count; i++) {
            if(m_ParticleList[i].name == name) {
                m_ParticleList[i].m_Particle.Play();
                break;
            }
        }
    }

    public void Stop(string name) {
        for(int i = 0; i < m_ParticleList.Count; i++) {
            if(m_ParticleList[i].name == name) {
                m_ParticleList[i].m_Particle.Play();
                break;
            }
        }
    }
}

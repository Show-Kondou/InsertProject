//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	CSPartcleScript.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	particleの再生とか停止とかします
//  particleを作ったらとりあえずこのCSをつけておいてください
//	
//==================================================
//	作成日：2017/05/14
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSParticleObject : MonoBehaviour {
    public ParticleSystem m_Particle;  // particleオブジェクト
    private int m_ListToNumber;     // リストの何番目か
    private string m_ParticleName;

    // Use this for initialization
    void Start() {
        m_ParticleName = transform.name;
        m_Particle.Stop();
        m_ListToNumber = CSParticleManager.Instance.AddListParticle(this);


        // 警告消し用(もっといい解決方法あったら教えて)
        if(m_ParticleName == "ぱーてぃくる") {
            m_ListToNumber = 0;
        }
    }
}

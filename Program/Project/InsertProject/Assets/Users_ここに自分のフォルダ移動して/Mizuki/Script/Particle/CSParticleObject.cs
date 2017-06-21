//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	CSPartcleObject.cs
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

public class CSParticleObject : ObjectBase {
	[HideInInspector]
    public ParticleSystem m_Particle;  // particleオブジェクト
	public ParticleSystem m_ParticlePrefab;
    public int m_ListToNumber;     // リストの何番目か
    public CSParticleManager.PARTICLE_TYPE m_ParticleType;		// お名前
	public int m_nNumberOfSameType;    // 同じ名前のパーティクルで何番目か
	public int nNumberOfSameType {
		set
		{
			m_nNumberOfSameType = value;
		}
		get
		{
			return m_nNumberOfSameType;
		}
	}


    // Use this for initialization
    void Start() {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
    }

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="type">パーティクルタイプ</param>
	/// <param name="nNumber">同名パーティクルの何番目か</param>
	public void Create(CSParticleManager.PARTICLE_TYPE type, int nNumber) {
		m_Particle = Instantiate(m_ParticlePrefab);
        m_ListToNumber = CSParticleManager.Instance.AddListParticle(this);
		m_ParticleType = type;
		m_nNumberOfSameType = nNumber;
		m_Particle.Stop();
		m_Particle.transform.parent = gameObject.transform;
	}
}

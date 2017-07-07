using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAddEffect : MonoBehaviour {



	private bool m_isUse = false;

	RectTransform m_TargetPos;

	RectTransform m_MyRect;


	private float m_Speed = 10.0F;

	private Vector3 m_InitVec;

	private Vector3 m_NowVec;

	
	private FiverGauge m_FeverGauge;

	public bool IsUse {
		get { return m_isUse; }
	}

	public Vector3 SetPos {
		set { m_MyRect.position = value; }
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void myInit( FiverGauge f ) {
		m_isUse = false;
		var rect = GetComponent<RectTransform>();
		if( !rect ) {
			Debug.LogError("RectTransformの取得に失敗");
		}
		if( !f ) {
			Debug.LogError( "フィーバーゲージの取得に失敗" );
		}
		m_FeverGauge = f;
		m_MyRect = rect;
		m_MyRect.sizeDelta = new Vector2( 0.5F, 0.5F );
		m_InitVec.x = Random.Range( -10.0F, 10.0F );
		m_InitVec.y = 0.0F;
		m_InitVec.z = Random.Range( -10.0F, 10.0F );
		m_InitVec /= 25.0F;

	}


	/// <summary>
	/// 更新
	/// </summary>
	public void myUpdate() {
		if( !m_isUse ) return;
		// ポジジョン計算
		// ベクトル減衰
		m_NowVec += (Vector3.zero - m_NowVec) * Time.deltaTime ;
		var pos = m_MyRect.position;
		pos += (m_TargetPos.position - pos) / 2.0F * Time.deltaTime * m_Speed + m_NowVec;
		m_MyRect.position = pos;
		var d = m_MyRect.position - m_TargetPos.position;
		if( d.magnitude < 0.1F ) {
			m_isUse = false;
			m_FeverGauge.AddFiver( 0.8F );
		}

	}

	public void Play( RectTransform pos ) {
		m_TargetPos = pos;
		m_NowVec = m_InitVec;
		m_isUse = true;
	}


}

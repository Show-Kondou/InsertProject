using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverEffectMgr : MonoBehaviour {

	#region Singleton

	public static FeverEffectMgr _instance = null;

	public static FeverEffectMgr Instance {
		get {
			if( !_instance ) {
				// インスタンスの生成
				_instance = (FeverEffectMgr)FindObjectOfType( typeof( FeverEffectMgr ) );
				DontDestroyOnLoad( _instance );
				if( null == _instance )
					Debug.LogError( "生成失敗" );
			}
			return _instance;
		}
	}

	#endregion Singleton



	// 定数
	private readonly uint EFFECT_MAX = 100; // 生成エフェクト数

	// インスペクター設定
	[Header("エフェクトの最終地点(RectTransform)"), SerializeField]
	private RectTransform m_EndTargetPos;      // エフェクトの最終地点
	[Header("エフェクト"),SerializeField]
	private FeverAddEffect m_EffectOriginal;    // エフェクト本体
	[Header("ゲージ"), SerializeField]
	private FiverGauge m_FeverGauge;


	private List<FeverAddEffect> m_EffectList = new List<FeverAddEffect>();

	// Use this for initialization
	void Start () {
		for( int i = 0; i < EFFECT_MAX; i++ ) {
			var obj = Instantiate( m_EffectOriginal );
			obj.transform.parent = transform;
			obj.myInit( m_FeverGauge );
			obj.gameObject.SetActive( false );
			m_EffectList.Add( obj );
		}
	}


	public void PlayEffect( Vector3 pos, uint unEffectNum = 5 ) {
		int nCnt = 0;
		foreach( var i in m_EffectList ) {
			i.gameObject.SetActive( true );
			if( i.IsUse ) continue;
			i.SetPos = pos;
			i.Play( m_EndTargetPos );
			nCnt++;
			if( nCnt > unEffectNum ) break;
		}
	}
	

	void Update() {
		foreach( var i in m_EffectList ) {
			i.myUpdate();
		}
		if( Input.GetKeyDown( KeyCode.W ) ) {
			PlayEffect(transform.position);
		}
	}
	
}

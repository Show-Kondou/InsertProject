
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CFadeBase]
//
//  ファイル名：CFadeBase.cs
//  説　　　明：フェードクラスのベース
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  11/10　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;



// ======================================
//	構造体
// ======================================
/* フェードの状態 */
[System.Serializable]
public enum FADE_TYPE {
	OFF,        // フェードオフ
	OUT,		// フェードアウト
	CHANGE,		// フェードチェンジ
	IN,			// フェードイン
	END,		// フェードエンド
}



// ======================================
//  CFadeBase
// ======================================
public class CFadeBase : MonoBehaviour {
	// ===== メンバ =====
	#region Member
	[Header("フェードアウトの時間")]
	[SerializeField]
	protected float m_FadeOutTime;
	[Header("フェードインの時間")]
	[SerializeField]
	protected float m_FadeInTime;
	[Header("フェード経過時間")]
	[SerializeField]
	protected float m_Time;
	[Header("フェードの状態")]
	[SerializeField]
	protected FADE_TYPE m_FadeType = FADE_TYPE.OFF;
	[Header("キャンバス")]
	[SerializeField]
	protected Canvas m_Canvas = null;
	[Header("フェードアウト割合"),SerializeField]
	protected float m_FadeOutRatio = 0.0F; // ０～ １で変化 １で完了
	[Header("フェードイン割合"),SerializeField]
	protected float m_FadeInRatio = 0.0F;

	#endregion Menbar


	// ===== アクセサ =====
	#region Accessor
	public FADE_TYPE FadeType { get { return m_FadeType; } }
	protected bool IsFade { get { return FADE_TYPE.OFF != m_FadeType; } }
	protected bool IsFadeOut { get { return m_FadeType == FADE_TYPE.OUT; } }
	protected bool IsFadeIn { get { return m_FadeType == FADE_TYPE.IN; } }
	#endregion Accessor


	// ===== メソッド =====
	#region Method
	// //////////////////////////////
	//  [FadeOut]
	//  ・フェードアウト開始関数
	//    in ：none
	//    out：void
	//
	public void FadeOut() {
		if( IsFade ) {
			//Debug.Log( "フェードエラー" );
			return;
		}
		m_Time = 0.0F;
		m_FadeType = FADE_TYPE.OUT;

		m_Canvas.sortingOrder = 3;
	}


	// //////////////////////////////
	//  [FadeIn]
	//  ・フェードイン開始関数
	//    in ：none
	//    out：void
	//
	public void FadeIn() {
		if( FADE_TYPE.CHANGE != m_FadeType ) {
			//Debug.Log( "フェードエラー" );
			return;
		}
		m_Time = 0.0F;
		m_FadeType = FADE_TYPE.IN;


	}
	
	#endregion Method


	#region VirtualMethod
	// //////////////////////////////
	//  [FadeInit] - 仮想関数
	//  ・フェードの初期化
	//    in ：none
	//    out：void
	//
	public virtual void FadeInit() {
		m_FadeType = FADE_TYPE.OFF;
		m_Time = 0.0F;
	}


	// //////////////////////////////
	//  [FadeUpdate] - 仮想関数
	//  ・フェードの更新
	//    in ：none
	//    out：void
	//
	public virtual void FadeUpdate() {
		if( IsFadeOut ) {
			FadeOutUpdate();
		}
		if( IsFadeIn ) {
			FadeInUpdate();
		}
		m_Time += Time.unscaledDeltaTime;
	}


	// //////////////////////////////
	//  [FadeOutUpdate] - 仮想関数
	//  ・フェードアウトの更新
	//    in ：none
	//    out：void
	//
	protected virtual void FadeOutUpdate() {
		if( m_FadeOutTime <= m_Time ) {
			m_FadeType = FADE_TYPE.CHANGE;
		}
		m_FadeOutRatio = m_Time / m_FadeOutTime;
	}


	// //////////////////////////////
	//  [FadeInUpdate] - 仮想関数
	//  ・フェードインの更新
	//    in ：none
	//    out：void
	//
	protected virtual void FadeInUpdate() {
		if( m_FadeInTime <= m_Time ) {
			m_FadeType = FADE_TYPE.END;
			m_Canvas.sortingOrder = -1;
		}
		m_FadeInRatio = m_Time / m_FadeInTime;
	}

	#endregion VirtualMethod
}

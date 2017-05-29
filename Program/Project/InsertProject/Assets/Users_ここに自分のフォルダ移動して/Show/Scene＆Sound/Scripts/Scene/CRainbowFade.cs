
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CRainbowFade]
//
//  ファイル名：CRainbowFade.cs
//  説　　　明：虹フェードクラス
//  制　作　者：Show Kondou	
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  11/10　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;
using UnityEngine.UI;


// ======================================
//  CRainbowFade
// ======================================
public class CRainbowFade : CFadeBase {
	// ===== 定数 =====

	// ===== メンバ =====
	public Image m_Image;
	private Vector3 m_InitPos;
	private Vector3 m_Center = Vector3.zero;
	private Vector3 m_EndPos;



	// ===== メソッド =====
	void Start() {
		m_InitPos = m_Image.rectTransform.localPosition;
		m_EndPos = m_InitPos * -1.0F;

	}

	// //////////////////////////////
	//  [FadeInit]
	//  ・
	//    in ：none
	//    out：void
	//
	public override void FadeInit() {
		base.FadeInit();
		if( null == m_Image ) {
			Debug.Log( "イメージが設定されていません" );
			return;
		}
	}



	// //////////////////////////////
	//  [FadeOutUpdate]
	//  ・
	//    in ：none
	//    out：void
	//
	protected override void FadeOutUpdate() {
		base.FadeOutUpdate();
		float t = 1 - ((m_FadeOutTime - m_Time)/ m_FadeOutTime);
		var pos = Vector3.Lerp( m_InitPos, m_Center, t );
		m_Image.rectTransform.localPosition = pos;
	}



	// //////////////////////////////
	//  [FadeInUpdate]
	//  ・
	//    in ：none
	//    out：void
	//
	protected override void FadeInUpdate() {
		base.FadeInUpdate();
		float t = 1 - ((m_FadeInTime - m_Time) / m_FadeInTime);
		var pos = Vector3.Lerp( m_Center, m_EndPos, t );
		m_Image.rectTransform.localPosition = pos;
	}


}

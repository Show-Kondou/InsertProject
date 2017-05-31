//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CBlackFade]
//
//  ファイル名：CBlackFade.cs
//  説　　　明：黒フェード
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
//  CBlackFade
// ======================================
public class CBlackFade : CFadeBase {
	// ===== メンバ =====
	public Image m_Image; // 黒テクスチャ



	// ===== メソッド =====

	// //////////////////////////////
	//  [Start]
	//  ・スタート関数
	//    in ：none
	//    out：void
	//
	void Start() {
		FadeInit();
	}



	// //////////////////////////////
	//  [FadeInit]
	//  ・初期化関数
	//    in ：none
	//    out：void
	//
	public override void FadeInit() {
		base.FadeInit();
		if( null == m_Image ) {
			Debug.Log( "イメージの取得に失敗" );
		}
	}



	// //////////////////////////////
	//  [FadeOutUpdate]
	//  ・フェードアウト更新
	//    in ：none
	//    out：void
	//
	protected override void FadeOutUpdate() {
		base.FadeOutUpdate();
		float alpha = m_FadeOutRatio;
		Color color = m_Image.color;
		color.a = alpha;
		m_Image.color = color;
	}



	// //////////////////////////////
	//  [FadeInUpdate]
	//  ・フェードインの更新
	//    in ：none
	//    out：void
	//
	protected override void FadeInUpdate() {
		base.FadeInUpdate();
		float alpha = 1 - m_FadeInRatio;
		Color color = m_Image.color;
		color.a = alpha;
		m_Image.color = color;
	}


}


//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CAudioData]
//
//  ファイル名：CAudioData.cs
//  説　　　明：オーディオ関係のデータ
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  12/07　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;
using System.Collections.Generic;


// ===== 定数 =====
public enum AUDIO_LIST {
	/* BGM */
	BGM_BOSS,			// ボスBGM
	BGM_GAMEOVER,		// ゲームオーバー
	BGM_MAIN,			// メインのBGM
	BGM_PINCH,			// ピンチ
	BGM_TITLE_0,		// タイトルBGM０
	BGM_TITLE_1,		// タイトルBGM１
	BGM_TITLE_2,		// タイトルBGM２
	BGM_TITLE_3,		// タイトルBGM３
	BGM_TITLE_4,		// タイトルBGM４
	
	/* SE */
	SE_BOSS_ATTACK_BRAINWASHING,	// ボス攻洗脳
	SE_BOSS_ATTACK_CANCEL,			// ボスの攻撃キャンセル
	SE_BOSS_ATTACK_SLIME,			// ボスの特殊攻撃
	SE_BOSS_HIT_0,					// ボスヒット音０
	SE_BOSS_HIT_1,					// ボスヒット音１
	SE_BOSS_KILL,					// ボス死亡
	SE_BOSS_SYUTUGEN,				// ボス生成
	SE_ENTER_0,						// 決定音
	SE_ENTER_1,						// 決定音
	SE_FEVER_FULL,					// フィーバーゲージ満タン
	SE_FEVER_UP,					// フィーバーゲージ回復
	SE_LINEDRAW,
	SE_LINEDRAW_WAIT,
	SE_MAGICWALL,					// 魔法壁生成SE
	SE_MAGICWALL_MOVE,				// 魔法壁生成SE
	SE_MAGICWALL_OVER,              // 魔法壁生成SE
	SE_MAGICWALL_POP,               // 魔法壁生成SE
	SE_MAGICWALL_GATTAI,				// 魔法壁生成SE
	SE_SLIME_CONVERT,
	SE_SLIME_OVER,					// スライムの数が多い警告音

	/* 最大数 */
	MAX
}



// ======================================
//  CAudioData
// ======================================
public class CAudioData {
	// ===== メンバ =====
	// 基本ファイルパス
	static string FILE_PATH = "Sound/";// = "Assets/Resources/Sound/";

	// オーディオファイル名
	static List<string> m_FileName =  new List<string>{
		/* BGM */
		"BGM_BOSS",			// ボスBGM
		"BGM_GAMEOVER",		// ゲームオーバー
		"BGM_MAIN",			// メインのBGM
		"BGM_PINCH",			// ピンチ
		"BGM_TITLE_0",		// タイトルBGM０
		"BGM_TITLE_1",		// タイトルBGM１
		"BGM_TITLE_2",		// タイトルBGM２
		"BGM_TITLE_3",		// タイトルBGM３
		"BGM_TITLE_4",		// タイトルBGM４
	
		/* SE */
		"SE_BOSS_ATTACK_BRAINWASHING",	// ボス攻洗脳
		"SE_BOSS_ATTACK_CANCEL",			// ボスの攻撃キャンセル
		"SE_BOSS_ATTACK_SLIME",			// ボスの特殊攻撃
		"SE_BOSS_HIT_0",					// ボスヒット音０
		"SE_BOSS_HIT_1",					// ボスヒット音１
		"SE_BOSS_KILL",					// ボス死亡
		"SE_BOSS_SYUTUGEN",				// ボス生成
		"SE_ENTER_0",						// 決定音
		"SE_ENTER_1",						// 決定音
		"SE_FEVER_FULL",					// フィーバーゲージ満タン
		"SE_FEVER_UP",					// フィーバーゲージ回復
		"SE_LINEDRAW",
		"SE_LINEDRAW_WAIT",
		"SE_MAGICWALL",					// 魔法壁生成SE
		"SE_MAGICWALL_MOVE",				// 魔法壁生成SE
		"SE_MAGICWALL_OVER",				// 魔法壁生成SE
		"SE_MAGICWALL_POP",				// 魔法壁生成SE
		"SE_MAGICWALL_GATTAI",				// 魔法壁生成SE
		"SE_SLIME_CONVERT",
		"SE_SLIME_OVER",					// スライムの数が多い警告音
		null,
	};



	// ===== メソッド =====

	// //////////////////////////////
	//  [CAudioData]
	//  ・コンストラクタ
	//    in ：none
	//    out：none
	//
	CAudioData() {
		if( m_FileName.Count != (int)AUDIO_LIST.MAX ) {
			Debug.Log( "オーディオのファイル数と定数が一致しませんでした。" );
			return;
		}
	}




	// //////////////////////////////
	//  [GetFileName]
	//  ・ファイル名取得
	//    in ：AUDIO_LIST
	//    out：string
	//
	static public string GetFileName( AUDIO_LIST value ) {
		return FILE_PATH + m_FileName[(int)value];
	}
	// int
	static public string GetFileName( int value ) {
		return FILE_PATH + m_FileName[value];
	}

}

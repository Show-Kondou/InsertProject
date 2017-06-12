﻿
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
	BGM_TITLE_0,		// タイトルBGM０
	BGM_TITLE_1,		// タイトルBGM１
	BGM_TITLE_2,		// タイトルBGM２
	BGM_TITLE_3,		// タイトルBGM３
	BGM_TITLE_4,		// タイトルBGM４
	BGM_MAIN,			// メインのBGM
	BGM_BOSS,			// ボスBGM
	BGM_PINCH_0,		// ピンチ１
	BGM_PINCH_1,		// ピンチ２
	BGM_GAME_OVER,		// ゲームオーバー
	
	/* SE */
	SE_MAGIC,			// 魔法壁生成SE
	SE_FEVER_UP,		// フィーバーゲージ回復
	SE_FEVER_FURU,		// フィーバーゲージ満タン
	SE_ENTER_0,			// 決定音
	SE_ENTER_1,         // 決定音
	SE_SLIME_OVER,		// スライムの数が多い警告音
	SE_BOSS_ATTACK,		// ボス攻撃音
	SE_BOSS_ATTACK_SP,	// ボスの特殊攻撃
	SE_BOSS_ATTACK_CANCEL,	// ボスの攻撃キャンセル
	SE_BOSS_HIT_0,		// ボスヒット音
	SE_BOSS_HIT_1,      // ボスヒット音
	SE_BOSS_KILL,       // ボス死亡

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
		"BGM_TITLE_0",		// タイトルBGM０
		"BGM_TITLE_1",		// タイトルBGM１
		"BGM_TITLE_2",		// タイトルBGM２
		"BGM_TITLE_3",		// タイトルBGM３
		"BGM_TITLE_4",		// タイトルBGM４
		"BGM_MAIN",			// メインのBGM
		"BGM_BOSS",			// ボスBGM
		"BGM_PINCH_0",		// ピンチ１
		"BGM_PINCH_1",		// ピンチ２
		"BGM_GAMEOVER",	// ゲームオーバー
	
		/* SE */
		"SE_MAGIC",			// 魔法壁生成SE
		"SE_FEVER_UP",		// フィーバーゲージ回復
		"SE_FEVER_FURU",		// フィーバーゲージ満タン
		"SE_ENTER_0",			// 決定音
		"SE_ENTER_1",         // 決定音
		"SE_SLIME_OVER",		// スライムの数が多い警告音
		"SE_BOSS_ATTACK",		// ボス攻撃音
		"SE_BOSS_ATTACK_SP",	// ボスの特殊攻撃
		"SE_BOSS_ATTACK_CANCEL",	// ボスの攻撃キャンセル
		"SE_BOSS_HIT_0",		// ボスヒット音
		"SE_BOSS_HIT_1",      // ボスヒット音
		"SE_BOSS_KILL",       // ボス死亡
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

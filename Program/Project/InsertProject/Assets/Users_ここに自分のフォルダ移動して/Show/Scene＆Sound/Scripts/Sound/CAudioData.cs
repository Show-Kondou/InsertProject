
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
	BGM_T,		// タイトルBGM
	BGM_SS,		// ステージセレクト
	BGM_DB_0,	// ドラゴンバスターBGM０
	BGM_DB_1,   // ドラゴンバスターBGM１
	BGM_DB_2,   // ドラゴンバスターBGM２
	BGM_DA_0,	// ドルアーガBGM０
	BGM_DA_1,   // ドルアーガBGM１
	BGM_DA_2,   // ドルアーガBGM２
	BGM_CLEAR,   // ドルアーガBGM２
	BGM_OVER,   // ドルアーガBGM２

	/* SE */
	SE01,
	SE02,
	SE03,
	SE04,
	SE05,
	SE06,
	SE07,
	SE08,
	SE09,
	SE10,
	SE11,
	SE12,
	SE13,
	SE14,
	SE15,
	SE16,
	SE17,
	SE18,
	SE19,
	SE20,
	SE21,
	SE22,
	SE23,
	SE24,
	SE25,
	SE26,
	SE27,
	SE28,
	SE29,
	SE30,
	SE31,
	SE32,
	SE33,

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
		"BGM01",
		"BGM02",
		"BGM03",
		"BGM04",
		"BGM05",
		"BGM06",
		"BGM07",
		"BGM08",
		"BGM09",
		"BGM10",

		/* SE */
		"SE01",
		"SE02",
		"SE03",
		"SE04",
		"SE05",
		"SE06",
		"SE07",
		"SE08",
		"SE09",
		"SE10",
		"SE11",
		"SE12",
		"SE13",
		"SE14",
		"SE15",
		"SE16",
		"SE17",
		"SE18",
		"SE19",
		"SE20",
		"SE21",
		"SE22",
		"SE23",
		"SE24",
		"SE25",
		"SE26",
		"SE27",
		"SE28",
		"SE29",
		"SE30",
		"SE31",
		"SE32",
		"SE33",
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

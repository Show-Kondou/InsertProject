
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CSceneManager]
//
//  ファイル名：CSceneManager.cs
//  説　　　明：シーンマネージャー
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  01/20　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
// ======================================
//  using
// ======================================
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



// ======================================
//	定数
// ======================================
/* シーンの番号 */
public enum SCENE {
	TITLE = 0,      // タイトル
	GAME,
	RESULT,
	ANY,			// 任意のシーンを選択
	MAX,            // シーンの最大数
}

/* フェード番号 */
public enum FADE {
	BLACK = 0,  // 黒フェード
	RAINBOW,    // 虹フェード
}



// ======================================
//  CSceneManager
// ======================================
public class CSceneManager : MonoBehaviour {
	#region Singleton
	private static CSceneManager _instance; // インスタンス

	// インスタンスアクセサ
	public static CSceneManager Instance {
		get {
			// インスタンスの生成チェック
			if( null == _instance ) {
				// インスタンスの生成
				_instance = (CSceneManager)FindObjectOfType( typeof( CSceneManager ) );
				if( null == _instance )
					Debug.Log( "シーンマネージャーのインスタンス生成に失敗" );
			}
			return _instance;
		}
	}
	#endregion Singleton

	// ===== メンバ =====
	[Header("現在のシーン番号")]
	[SerializeField]
	private SCENE m_NowScene = SCENE.TITLE;
	[Header("現在のフェード")]
	[SerializeField]
	private CFadeBase m_NowFade = null;
	[Header("シーンの名前")]
	[SerializeField]
	private string[] m_Scenes = {
													"Title",
													"Game",
													"Result",
													"Any",	// 可変シーン(デバッグ用)
													};
	[Header("フェードリスト")]
	public List<CFadeBase>  m_FadeList = new List<CFadeBase>();     // フェードのリスト
	private SCENE m_NextScene;                                      // 次のシーン番号
	private LoadSceneMode m_NextLoadMode = LoadSceneMode.Single;    // 次のシーン読み込み方法
	private List<CFadeBase> m_FadeInstance = new List<CFadeBase>(); // フェードのインスタンス
	private bool m_isChangeScene = false;


	public bool IsChange {
		get { return m_isChangeScene; }
	}
	



	// ===== メソッド =====

	// //////////////////////////////
	//  [Awake]
	//  ・説明
	//    in ：void
	//    out：void
	//
	private void Awake() {
		// インスタンスチェック
		if( this != Instance ) {
			Destroy( gameObject );
			return;
		}
		// 保存シーンに移行
		DontDestroyOnLoad( gameObject );
	}



	// //////////////////////////////
	//  [Start]
	//  ・スタート関数
	//    in ：none
	//    out：void
	//
	void Start() {
		Init();
	}



	// //////////////////////////////
	//  [Init]
	//  ・初期化処理
	//    in ：none
	//    out：void
	//
	private void Init() {
		// フェードを生成
		foreach( var i in m_FadeList ) {
			CFadeBase fade = Instantiate( i, transform ) as CFadeBase;
			m_FadeInstance.Add( fade );
		}
	}



	// //////////////////////////////
	//  [LoadScene]
	//  ・説明
	//    in ：SCENE
	//    out：void
	//
	public void LoadScene( SCENE scene, FADE fade, LoadSceneMode loadSceneMode = LoadSceneMode.Single ) {
		m_NextScene = scene;
		m_NextLoadMode = loadSceneMode;
		m_NowFade = m_FadeInstance[(int)fade];
		m_NowFade.FadeOut();
		Time.timeScale = 0.0F;
	}


	// //////////////////////////////
	//  [LoadScene]
	//  ・説明
	//    in ：SCENE
	//    out：void
	//
	public void LoadScene( string sceneName, FADE fade, LoadSceneMode loadSceneMode = LoadSceneMode.Single ) {
		m_NextScene = SCENE.ANY;
		m_Scenes[(int)m_NextScene] = sceneName;
		m_NextLoadMode = loadSceneMode;
		m_NowFade = m_FadeInstance[(int)fade];
		m_NowFade.FadeOut();
		Time.timeScale = 0.0F;
	}



	// //////////////////////////////
	//  [ChangeScene]
	//  ・シーンのチェンジタイミング
	//    in ：none
	//    out：void
	//
	private void ChangeScene() {
		// シーンチェンジ
		SceneManager.LoadScene( m_Scenes[(int)m_NextScene], m_NextLoadMode );
		m_NowScene = m_NextScene;   // シーン番号更新
		m_NowFade.FadeIn();
	}



	// //////////////////////////////
	//  [Update]
	//  ・更新処理
	//    in ：none
	//    out：void
	//
	void Update() {
		FadeUpdate();
	}



	// //////////////////////////////
	//  [FadeUpdate]
	//  ・フェードの更新
	//    in ：none
	//    out：void
	//
	private void FadeUpdate() {
		if( null == m_NowFade )
			return;
		m_isChangeScene = false;
		m_NowFade.FadeUpdate();
		switch( m_NowFade.FadeType ) {
		/* フェード切り替え */
		case FADE_TYPE.CHANGE:
			ChangeScene();
			m_isChangeScene = true;
			break;
		/* フェード終了 */
		case FADE_TYPE.END:
			m_NowFade.FadeInit();
			m_NowFade = null;
			Time.timeScale = 1.0F;
			break;
		}
	}

}
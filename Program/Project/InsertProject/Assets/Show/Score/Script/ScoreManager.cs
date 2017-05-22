using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	// 定数
	#region Constant
	readonly int MAX_SCORE = int.MaxValue;　// スコアの上限
	readonly int MIN_SCORE = int.MinValue;　// スコアの下限
	#endregion Constant


	// メンバー
	#region Member

	[Header("スコア"), SerializeField]
	private int m_Score = 0;

	#endregion Member


	// アクセサ
	#region Accessor
	/// <summary>
	/// スコアのアクセサ
	/// </summary>
	public int Score {
		get {　return m_Score; }
		set { m_Score = AddScore( value ); }
	}
	#endregion Accessor


	// メソッド
	#region Method
		/// <summary>
		/// スコアの加算関数
		/// </summary>
		/// <param name="AddScore">追加スコア</param>
		/// <returns>加算結果</returns>
	private int AddScore( int AddScore) {
		int NowScore = m_Score;
		NowScore += AddScore;
		if( NowScore > MAX_SCORE ) {
			Debug.Log( "スコアの上限突破！！！" );
			NowScore = MAX_SCORE;
		}
		if( NowScore < MIN_SCORE ) {
			Debug.Log( "スコアの下限突破！！！" );
			NowScore = MIN_SCORE;
		}
		return NowScore;
	}

	#endregion Method


	// イベント
	#region MonoBehaviour Event

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	#endregion MonoBehaviour Event

}

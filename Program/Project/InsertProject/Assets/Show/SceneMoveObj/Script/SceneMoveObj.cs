using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveObj : MonoBehaviour {

	#region Singleton

	public static SceneMoveObj _instance = null;

	public static SceneMoveObj Instance {
		get {
			if( !_instance ) {
				// インスタンスの生成
				_instance = (SceneMoveObj)FindObjectOfType( typeof( SceneMoveObj ) );
				DontDestroyOnLoad( _instance );
				if( null == _instance )
					Debug.LogError( "生成失敗" );
			}
			return _instance;
		}
	}

	#endregion Singleton

	// 定数
	#region Constant
	#endregion Constant


	// メンバー
	#region Member
	//[Header("-----シーン間をまたぐオブジェクト-----")]
	[Header("スコア")]
	public MoveData<int> m_Score;

	//[Header("-----シーン間をまたぐオブジェクト-----")]
	#endregion Member


	// アクセサ
	#region Accessor
	#endregion Accessor


	// メソッド
	#region Method
	#endregion Method

}


[System.Serializable]
public class MoveData<T> {
	private T m_Data;
	public T Data {
		get { return m_Data; }
		set { m_Data = value; }
	}
}
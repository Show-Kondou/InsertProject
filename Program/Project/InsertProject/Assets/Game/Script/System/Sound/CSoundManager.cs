
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//
//  [CSoundManager]
//
//  ファイル名：CSoundManager.cs
//  説　　　明：サウンドマネージャー
//  制　作　者：Show Kondou
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//  - 更新履歴 -
//  2016  12/07　… 新規作成
//
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



// ======================================
//  CSoundManager
// ======================================
public class CSoundManager : MonoBehaviour {

	#region Singleton
	static private GameObject       m_GameObj   = null; // new GameObject("SoundManager");
	static private CSoundManager    m_Instance	= null;

	public static CSoundManager Instance {
		get {
			if( m_Instance == null ) {
				if( m_GameObj == null ) {
					m_GameObj = new GameObject( "SoundManager" );
				}
				m_Instance = m_GameObj.AddComponent<CSoundManager>();

				//サウンドのリストを全ロード
				LoadSound();
				CreateChannel();
				DontDestroyOnLoad( m_GameObj );
			}
			return m_Instance;
		}
	}
	#endregion Singleton

	// ===== 定数 ====
	private const int BGM_CHANNEL = 2;
	private const int SE_CHANNEL  = 10;

	// ===== メンバ =====
	// オーディオクリップデータ
	static private List<AudioClip>		m_AudioList = new List<AudioClip>();
	// BGMのオーディオソース
	static private List<AudioSource>    m_BGMChannel = new List<AudioSource>();
	// SEのオーディオソース
	static private List<AudioSource>    m_SEChannel = new List<AudioSource>();


	// ===== メソッド =====


	// //////////////////////////////
	//  [LoadSound]
	//  ・サウンドのロード
	//    in ：none
	//    out：void
	//
	static private void LoadSound() {
		for( int i = 0; i < (int)AUDIO_LIST.MAX; i ++ ) {
			m_AudioList.Add( new AudioClip() );
			var data = Resources.Load( CAudioData.GetFileName( i ) );
			m_AudioList[i] = data as AudioClip;
			if( null == m_AudioList[i] ) {
				Debug.Log("オーディオクリップの取得に失敗\n" + CAudioData.GetFileName( i ) );
			}
		}
	}



	// //////////////////////////////
	//  [CreateChannel]
	//  ・オーディオチャンネルの生成
	//    in ：none
	//    out：void
	//
	static private void CreateChannel() {
		int i;
		// BGMオーディオソース作成
		for( i = 0; i < BGM_CHANNEL; i++ ) {
			var obj = new GameObject( "BGMChannel_" + i );
			obj.transform.parent = m_GameObj.transform;
			var channel = obj.AddComponent<AudioSource>();
			m_BGMChannel.Add( channel );
		}
		// SEオーディオソース作成
		for( i = 0; i < SE_CHANNEL; i++ ) {
			var obj = new GameObject( "SEChannel_" + i );
			obj.transform.parent = m_GameObj.transform;
			var channel = obj.AddComponent<AudioSource>();
			m_SEChannel.Add( channel );
		}
	}



	// //////////////////////////////
	//  [PlayBGM]
	//  ・BGM再生
	//    in ：	AUDIO_LIST	… 音の番号
	//			bool		… ループ設定
	//			bool		… フェード設定
	//    out：	void
	//
	public void PlayBGM( AUDIO_LIST value, bool isLoop = false, bool isFade = false ) {
		m_BGMChannel[0].clip = m_AudioList[(int)value];
		m_BGMChannel[0].loop = isLoop;
		m_BGMChannel[0].Play();
	}



	// //////////////////////////////
	//  [PlaySE]
	//  ・BGM再生
	//    in ：	AUDIO_LIST	… 音の番号
	//    out：	void
	//
	public void PlaySE( AUDIO_LIST value, bool isOneShot = true ) {
		foreach( var i in m_SEChannel ) {
			//if( value == AUDIO_LIST.SE_MAGICWALL_GATTAI ) {
			//	Debug.Log( "合体" );
			//}


			if( i.isPlaying ) {
				// Debug.Log( "だぶり？" );
				if( i.clip == null )　continue;
				if( i.clip.name == m_AudioList[(int)value].name ) {
					i.Stop();
					i.Play();
					 // Debug.Log("だぶり");
					return;
				}
				continue;
			} else {
				if( isOneShot ) {
					i.PlayOneShot( m_AudioList[(int)value] );
				} else {
					i.clip = m_AudioList[(int)value];
					i.loop = false;
					i.Play();
				}
				return;
			}
		}
	}

	/// <summary>
	/// 同じ音が流れないようにし、同じ音なら
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	private bool CheckAudio( AudioSource check, AUDIO_LIST value ) {
		if( !check.isPlaying ) return false;
		Debug.Log( check.clip.name + m_AudioList[(int)value].name );

		if( check.name == m_AudioList[(int)value].name ) {
			Debug.Log( check.name + m_AudioList[(int)value].name );
			check.Stop();
			check.Play();
			// Debug.Log("だぶり");
			return false;
		}else {
			return true;
		}
	}



	public void StopBGM( AUDIO_LIST value ) {
		foreach( var i in m_BGMChannel ) {
			if( !i.isPlaying ) continue;
			if( i.clip.name != m_AudioList[(int)value].name )
				continue;
			i.clip = m_AudioList[(int)value];
			i.Stop();
			break;
		}
	}

	public void StopBGM() {
		foreach( var i in m_BGMChannel) {
			// Debug.Log( i.name );
			i.Stop();
		}
	}

	public void PlayThunder() {
		m_SEChannel[SE_CHANNEL - 1].Play();

	}

	public void StopThunder() {
		m_SEChannel[SE_CHANNEL - 1].Stop();
	}




}

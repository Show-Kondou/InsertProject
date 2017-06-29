using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour {

	[Header("時計"),SerializeField]
	private TimeLimit timeLimit;

	[Header("ライン描画当たり判定"), SerializeField]
	private Collider coll;


	// Use this for initialization
	void Start () {
		if( !timeLimit ) {
			Debug.LogError("時計クラス取得失敗");
		}
		if( !coll ) {
			Debug.LogError("ライン当たり判定取得失敗");
		}
		
	}
	

	public void StartGameEvent() {
		timeLimit.bTimeStop = false;
		coll.enabled = true;

	}

	public void StopGameEvent() {
		timeLimit.bTimeStop = true;
		coll.enabled = false;
	}
}

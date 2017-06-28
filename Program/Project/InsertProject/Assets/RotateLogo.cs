using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLogo : MonoBehaviour {

	//回転管理用フラグ
	public bool bXPlus;
	public bool bYPlus;
	public bool bZPlus;
	public bool bXMinus;
	public bool bYMinus;
	public bool bZMinus;
	public bool bDist;

	//回転スピード
	public float fRotateSpeed;

	//プレス機フラグ
	private bool bMoveFirst;
	private bool bVisible;

	//親オブジェクト
	GameObject gParentObj;
	

	//回転制御用関連
	private bool bRotateReturn = false;
	public Vector3 vRotate = new Vector3(0.0f, 0.0f, 0.0f);
	void Start() {
		

		//親オブジェ取得
		gParentObj = gameObject.transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update() {

		if( bMoveFirst == false ) {

			if( bXPlus == true ) {
				transform.Rotate( new Vector3( fRotateSpeed * Time.deltaTime, 0.0f, 0.0f ) );
				vRotate -= new Vector3( fRotateSpeed * Time.deltaTime, 0.0f, 0.0f );
			}

			if( bYPlus == true ) {
				transform.Rotate( new Vector3( 0.0f, fRotateSpeed * Time.deltaTime, 0.0f ) );
				vRotate -= new Vector3( 0.0f, fRotateSpeed * Time.deltaTime, 0.0f );
			}

			if( bZPlus == true ) {
				transform.Rotate( new Vector3( 0.0f, 0.0f, fRotateSpeed * Time.deltaTime ) );
				vRotate -= new Vector3( 0.0f, 0.0f, fRotateSpeed * Time.deltaTime );
			}

			if( bXMinus == true ) {
				transform.Rotate( new Vector3( -fRotateSpeed * Time.deltaTime, 0.0f, 0.0f ) );
				vRotate += new Vector3( -fRotateSpeed * Time.deltaTime, 0.0f, 0.0f );
			}

			if( bYMinus == true ) {
				transform.Rotate( new Vector3( 0.0f, -fRotateSpeed * Time.deltaTime, 0.0f ) );
				vRotate += new Vector3( 0.0f, -fRotateSpeed * Time.deltaTime, 0.0f );
			}

			if( bZMinus == true ) {
				transform.Rotate( new Vector3( 0.0f, 0.0f, -fRotateSpeed * Time.deltaTime ) );
				vRotate += new Vector3( 0.0f, 0.0f, -fRotateSpeed * Time.deltaTime );
			}
		}

		

	}

}

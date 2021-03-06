﻿//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CFeverSlimeMovePointManager.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	フィーバースライムの移動位置を管理する。(画面周回)
//	
//==================================================
//	作成日：2017/06/15
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFeverSlimeMovePointManager : ObjectBase {
	// 位置列挙型
	public enum FEVER_SLIME_MOVE{
		LEFT_BOTTOM = 0,
		RIGHT_BOTTOM,
		RIGHT_UP,
		LEFT_UP,

		MAX
	}
	[SerializeField]
	private List<GameObject> FeverSlimeMovePoint = new List<GameObject>();  // 位置リスト
	private int targetNum;	// 目的地番号
	// 目的地ゲッター
	public Vector3 FeverSlimeDestination {
		get
		{
			return FeverSlimeMovePoint[targetNum].transform.position;
		}
	}

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		foreach(GameObject pos in FeverSlimeMovePoint) {
			pos.SetActive(false);
		}
	}

	/// <summary>
	/// 目的地変更
	/// </summary>
	/// <param name="prevNumber">現在目的地</param>
	public void ChangeNextPosition(int prevNumber) {
		FeverSlimeMovePoint[prevNumber].SetActive(false);
		FeverSlimeMovePoint[(prevNumber+1) % (int)FEVER_SLIME_MOVE.MAX].SetActive(true);
		targetNum = (prevNumber+1) % (int)FEVER_SLIME_MOVE.MAX;
	}
}

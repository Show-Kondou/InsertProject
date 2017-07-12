
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	CFerverSlimeMovePoint.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	フィーバースライムの移動位置を変更する。(画面周回)
//	
//==================================================
//	作成日：2017/06/15
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFeverSlimeMovePoint : MonoBehaviour {
	[SerializeField]
	CFeverSlimeMovePointManager mngObject;	// マネージャオブジェクト
	[SerializeField]
	CFeverSlimeMovePointManager.FEVER_SLIME_MOVE TargetID;  // 現在の目的地ID

	public void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag != "Fever")
			return;
		// 移動目的地変更
		mngObject.ChangeNextPosition((int)TargetID);
	}
}

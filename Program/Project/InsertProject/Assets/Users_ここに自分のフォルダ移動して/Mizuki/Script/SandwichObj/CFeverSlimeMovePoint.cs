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

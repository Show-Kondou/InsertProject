using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFeverSlimeMovePoint : MonoBehaviour {
	[SerializeField]
	CFeverSlimeMovePointManager mngObject;	// マネージャオブジェクト
	[SerializeField]
	CFeverSlimeMovePointManager.FEVER_SLIME_MOVE myPosition;	// 自身の位置

	public void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag != "Fever")
			return;
		// 移動位置変更
		mngObject.ChangeNextPosition((int)myPosition);
	}
}

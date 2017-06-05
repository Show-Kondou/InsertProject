using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===== 板ポリをカメラの方向に向けるスクリプト =====
public class BillboarCamera : MonoBehaviour 
{
    [SerializeField]
    private GameObject targetCamera;    // 正面を向くカメラ

	// ===== スタート関数 =====
	void Start () 
    {
        //対象のカメラが指定されない場合にはMainCameraを対象とします。
        if (this.targetCamera == null)
            targetCamera = GameObject.Find("Main Camera");

        //カメラの方向を向くようにする。
        this.transform.LookAt(-this.targetCamera.transform.position);
	}
	
	// ===== 更新関数 =====
	void Update () 
    {  	
	}
}

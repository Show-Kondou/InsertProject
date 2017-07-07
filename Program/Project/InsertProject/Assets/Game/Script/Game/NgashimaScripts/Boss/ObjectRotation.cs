using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour 
{
    [SerializeField]
    private Vector3 Rotation;

	// ===== スタート =====
	void Start () 
    {
		
	}
	
	// ===== 更新 =====
	void Update () 
    {
        // オブジェクトを回転させる
        this.transform.Rotate(Rotation.x, Rotation.y, Rotation.z);
	}
}

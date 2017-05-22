using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================
// 魔法陣を回転させるスクリプト
//=========================================
public class MagicCircle : MonoBehaviour 
{
    [SerializeField]
    private float fRotationSpeed;

	// スタート関数
	void Start () 
    {
		
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        transform.Rotate(0, 0, fRotationSpeed);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================
// 魔法陣を制御させるスクリプト
//=========================================
public class MagicCircle : MonoBehaviour 
{
    // ----- プライベート変数 -----
    private GameObject MagicCircleObj;  // 魔法陣オブジェクト
    private GameObject AuraEffectObj;   // オーラオブジェクト

    [SerializeField]
    private float fRotationSpeed;       // 魔法陣の回転スピード
    [SerializeField]
    private float fAlphaSpeed;          // 魔法陣のアルファ値変更スピード

    private bool bPillar = true;        // 柱出現フラグ
    private bool bAura = true;          // オーラ出現フラグ

    private float AuraTime = 0.0f;      // オーラエフェクトが出る時間カウント

	// スタート関数
	void Start () 
    {
        MagicCircleObj = this.transform.FindChild("MagicCircle").gameObject;
        AuraEffectObj = GameObject.Find("DevilEffect").transform.FindChild("AuraEffect").gameObject;
	}
	
	// ===== 更新関数 =====
	void Update () 
    {
        // 魔法陣を回転させる
        MagicCircleObj.transform.Rotate(0, 0, fRotationSpeed);

        if (MagicCircleObj.GetComponent<SpriteRenderer>().color.a <= 1.0f)
        {
            // 魔法陣のアルファ値を変更
            MagicCircleObj.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, fAlphaSpeed);
        }
        else
        {
            if (bPillar)
            {
                // 柱出現
                MagicCircleObj.transform.FindChild("Pillar").gameObject.SetActive(true);
                MagicCircleObj.transform.FindChild("Pillar2").gameObject.SetActive(true);
                MagicCircleObj.transform.FindChild("Pillar3").gameObject.SetActive(true);

                bPillar = false;
            }
        }

        AuraTime += Time.deltaTime;

        if (AuraTime >= 2.7f && bAura)
        {
            AuraEffectObj.SetActive(true);  // オーラ出現

            bAura = false;
        }
	}
}

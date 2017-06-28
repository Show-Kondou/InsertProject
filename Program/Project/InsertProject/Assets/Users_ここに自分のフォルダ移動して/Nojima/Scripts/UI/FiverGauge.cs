using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiverGauge : MonoBehaviour {

    //定数
    const float MAX_GAUGE = 42f;    //フィーバー最大値
    [SerializeField]
    float FALL_SPEED = 2f;          //ゲージが減るスピード

    //変数
    [SerializeField]
    Image FiverImg;
	
    bool bFullTank = false;         //ゲージ満タンフラグ
    bool bFiver = false;            //フィーバー中フラグ
    float PlayerGauge = 0f;         //プレイヤーのフィーバー値

    // Use this for initialization
    void Start() {
        FiverImg.fillAmount = 0f;
        FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
        FiverImg.material.color = new Color(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update() {  
        Fiver();                //フィーバー中の処理
    }

	/// <summary>
	/// フィーバーゲージ回復
	/// </summary>
	public void AddFiver(float add_fiver) 
	{
		PlayerGauge += add_fiver;
		FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
		CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_FEVER_UP );
		if(bFullTank)  //ゲージが満タンになった後に挟まれた時
			bFiver = true;
	}

    /// <summary>
    /// フィーバー
    /// </summary>
    void Fiver()
    {
        //フィーバーゲージ満タン
        if (PlayerGauge >= MAX_GAUGE)
        {
            bFullTank = true;
			CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_FEVER_FULL );
            FiverImg.material.color = new Color(0f, 1f, 0f);    //緑
			CSSandwichObjManager.Instance.CreateSandwichObj( CSSandwichObjManager.SandwichObjType.FeverSlime, transform.position );
		}
        //フィーバー中
        if (bFiver)
        {
            FallFiver(FALL_SPEED);
            bFullTank = false;
        }
        //フィーバーゲージゼロ
        if (PlayerGauge <= 0f)
        {
            FiverImg.material.color = new Color(1f, 1f, 1f);
            bFiver = false;
        }
    }

    /// <summary>
    /// フィーバーゲージ減少
    /// </summary>
    /// <param name="FallSpeed"></param>
    void FallFiver(float FallSpeed)
    {
            PlayerGauge -= FallSpeed * Time.deltaTime;
            FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
            FiverImg.material.color = new Color(1f, 0f, 0f);    //赤
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiverGauge : MonoBehaviour {

    //定数
    const float ADD_FIVER = 2f;     //フィーバー回復量
    const float MAX_GAUGE = 42f;    //フィーバー最大値
    [SerializeField]
    float FALL_SPEED = 2f;          //ゲージが減るスピード

    //変数
    [SerializeField]
    Image FiverImg;

    public bool bAddFiver = false;  //フィーバーフラグ
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
        AddFiver(ADD_FIVER);    //フィーバーゲージ回復    
        Fiver();                //フィーバー中の処理
    }

    /// <summary>
    /// フィーバーゲージ回復
    /// </summary>
    void AddFiver(float add_fiver)
    {
        if (bAddFiver)
        {
            PlayerGauge += add_fiver;
            FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
            if (bFullTank)  //ゲージが満タンになった後に挟まれた時
                bFiver = true;
            bAddFiver = false;
        }
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
            FiverImg.material.color = new Color(0f, 1f, 0f);    //緑
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiverGauge : MonoBehaviour {

    //定数
    const float ADD_FIVER = 2f;     //フィーバー回復量
    const float MAX_GAUGE = 42f;    //フィーバー最大値

    //変数
    [SerializeField]
    Image FiverImg;

    public bool bFiver = false;     //フィーバーフラグ
    float PlayerGauge = 0f;         //プレイヤーのフィーバー値

    // Use this for initialization
    void Start() {
        FiverImg.fillAmount = 0f;
        FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
    }

    // Update is called once per frame
    void Update() {
        AddFiver(ADD_FIVER);    //フィーバーゲージ回復    
        Fiver();                //フィーバー中の処理
        StopFiver();            //フィーバーが切れたとき
    }

    /// <summary>
    /// フィーバーゲージ回復
    /// </summary>
    void AddFiver(float add_fiver)
    {
        if (bFiver)
        {
            PlayerGauge += add_fiver;
            FiverImg.fillAmount = PlayerGauge / MAX_GAUGE;
            bFiver = false;
            //print("PlayerGauge" + PlayerGauge);
        }
    }

    /// <summary>
    /// フィーバー中
    /// </summary>
    void Fiver()
    {
        if (PlayerGauge >= MAX_GAUGE)
        {
            
        }
    }

    /// <summary>
    /// フィーバーが切れたとき
    /// </summary>
    void StopFiver()
    {
        //ゲージ切れたとき
        if (PlayerGauge > MAX_GAUGE)
        {
            PlayerGauge = 0f;
            FiverImg.fillAmount = PlayerGauge;
        }
    }
}

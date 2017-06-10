using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiverGauge : MonoBehaviour {

    //定数
    const float ADD_FIVER = 2f;  //フィーバー回復量

    //変数
    [SerializeField]
    Image FiverImg;

    [SerializeField]
    bool bFiver = false;
    float MAX_GAUGE = 48f;

    // Use this for initialization
    void Start() {
        FiverImg.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update() {
        AddFiver(ADD_FIVER); //フィーバーゲージ回復

        //ゲージ満タン
        if (FiverImg.fillAmount >= 1f)
            FiverImg.fillAmount = 0f;
    }

    /// <summary>
    /// フィーバーゲージ回復
    /// </summary>
    void AddFiver(float add_fiver)
    {
        if (bFiver)
        {
            FiverImg.fillAmount += 1f / add_fiver * Time.deltaTime;
            bFiver = false;
        }
    }
}

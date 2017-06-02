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


    // Use this for initialization
    void Start() {
        FiverImg.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update() {
        AddFiver(); //フィーバーゲージ回復

        //ゲージ満タン
        if (FiverImg.fillAmount >= 1f)
            FiverImg.fillAmount = 0f;
    }

    /// <summary>
    /// フィーバーゲージ回復
    /// </summary>
    void AddFiver()
    {
        if (bFiver)
        {
            FiverImg.fillAmount += 1f / ADD_FIVER * Time.deltaTime;
            bFiver = false;
        }
    }
}

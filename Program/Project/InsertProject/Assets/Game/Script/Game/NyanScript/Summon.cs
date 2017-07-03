using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour {

    //trueなら親falseなら子
    public bool bParent;

    //親オブジェクト
    GameObject gParentObj;

    //プレス機フラグ
    private bool bMoveFirst;

    //パーティクルシステム
    ParticleSystem pSummon;

    // Use this for initialization
    void Start () {

        if (bParent == true)
        {
            //親オブジェ取得
            gParentObj = gameObject.transform.parent.gameObject;
        }
        else if (bParent == false)
        {
            //親オブジェ取得
            gParentObj = gameObject.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update () {


        if (bParent == true)
        {
            //プレス機移動フラグ取得
            bMoveFirst = gParentObj.GetComponent<Press>().bMoveFirst;

            if (bMoveFirst == true)
            {
                pSummon = this.gameObject.GetComponent<ParticleSystem>();
                pSummon.Stop();
                Destroy(this.gameObject);
            }
        }
        else if (bParent == false)
        {
            //プレス機移動フラグ取得
            bMoveFirst = gParentObj.GetComponent<Summon>().bMoveFirst;

            if (bMoveFirst == true)
            {
                pSummon = this.gameObject.GetComponent<ParticleSystem>();
                pSummon.Stop();
                Destroy(this.gameObject);
            }
        }

    }
}

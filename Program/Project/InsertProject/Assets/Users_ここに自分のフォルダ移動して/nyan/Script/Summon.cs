using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour {

    //親オブジェクト
    GameObject gParentObj;

    //プレス機フラグ
    private bool bMoveFirst;

    // Use this for initialization
    void Start () {
        //親オブジェ取得
        gParentObj = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update () {

        //プレス機移動フラグ取得
        bMoveFirst = gParentObj.GetComponent<Press>().bMoveFirst;

        if (bMoveFirst == true)
        {
            Destroy(this.gameObject);
        }

    }
}

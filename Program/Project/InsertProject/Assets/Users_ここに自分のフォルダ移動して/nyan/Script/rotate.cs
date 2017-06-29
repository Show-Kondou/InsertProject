using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    //回転管理用フラグ
    public bool bXPlus;
    public bool bYPlus;
    public bool bZPlus;
    public bool bXMinus;
    public bool bYMinus;
    public bool bZMinus;
    public bool bDist;

    //回転スピード
    private float fRotateSpeed;

    //プレス機フラグ
    private bool bMoveFirst;
    private bool bVisible;

    //親オブジェクト
    GameObject gParentObj;

    //色変更
    private Renderer render;

    //回転制御用関連
    private bool bRotateReturn = false;
    public Vector3 vRotate = new Vector3(0.0f, 0.0f, 0.0f);

    //マテリアル変更用
    public Material[] materials;

    //変更制御用
    private bool bChangeMatCheck = false;
    private bool bChangeShaCheck = false;

    // Use this for initialization
    void Start () {

        fRotateSpeed = 400.0f;

        //親オブジェ取得
        gParentObj = gameObject.transform.parent.gameObject;

        render = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update () {

        //プレス機移動フラグ取得
        bMoveFirst = gParentObj.GetComponent<Press>().bMoveFirst;

        if (bMoveFirst == false)
        {

            if (bXPlus == true)
            {
                transform.Rotate(new Vector3(fRotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                vRotate -= new Vector3(fRotateSpeed * Time.deltaTime, 0.0f, 0.0f);
            }

            if (bYPlus == true)
            {
                transform.Rotate(new Vector3(0.0f, fRotateSpeed * Time.deltaTime, 0.0f));
                vRotate -= new Vector3(0.0f, fRotateSpeed * Time.deltaTime, 0.0f);
            }

            if (bZPlus == true)
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, fRotateSpeed * Time.deltaTime));
                vRotate -= new Vector3(0.0f, 0.0f, fRotateSpeed * Time.deltaTime);
            }

            if (bXMinus == true)
            {
                transform.Rotate(new Vector3(-fRotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                vRotate += new Vector3(-fRotateSpeed * Time.deltaTime, 0.0f, 0.0f);
            }

            if (bYMinus == true)
            {
                transform.Rotate(new Vector3(0.0f, -fRotateSpeed * Time.deltaTime, 0.0f));
                vRotate += new Vector3(0.0f, -fRotateSpeed * Time.deltaTime, 0.0f);
            }

            if (bZMinus == true)
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, -fRotateSpeed * Time.deltaTime));
                vRotate += new Vector3(0.0f, 0.0f, -fRotateSpeed * Time.deltaTime);
            }
        }
        else if (bMoveFirst == true)
        {
            if (bRotateReturn == false)
            {
                //回転軸を元に戻す
                transform.Rotate(vRotate);
                bRotateReturn = true;
            }
            else if (bRotateReturn == true)
            {
                if (bDist == true)
                {
                    transform.Rotate(new Vector3(fRotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                }
                else if (bDist == false)
                {
                    transform.Rotate(new Vector3(-fRotateSpeed * Time.deltaTime, 0.0f, 0.0f));
                }
            }
        }


        //プレス機非表示フラグ取得
        bVisible = gParentObj.GetComponent<Press>().bVisible;

        if (bVisible == true && bChangeShaCheck == false)
        {
            render.material.shader = Shader.Find("Standard");
            render.material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
            bChangeShaCheck = true;
        }


        //ぬるぬる判定マテリアル変更
        if (gParentObj.GetComponent<Press>().bNulnulS == true || gParentObj.GetComponent<Press>().bNulnulE == true)
        {
            if (bChangeMatCheck == false)
            {
                render.material = materials[1];
                bChangeMatCheck = true;
            }
        }
        //else
        //{
        //    render.material = materials[0];
        //}


    }
}

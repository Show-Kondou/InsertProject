using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour {

    //親オブジェクト
    GameObject gParentObj;

    //ID
    public int nPressID;
    public bool bWallStart;

    //リスト関連
    public List<Vector3> lvStorage = new List<Vector3>();
    private int nListCnt;
    private int nListCntDiv;
    private int nNextList;

    //座標
    public Vector3 vLookPos;
    private Vector3 vNewPos;
    private Vector3 vOldPos;

    //移動関連
    private Vector3 vSpeed = new Vector3(0.05f, 0.05f, 0.05f);
    private float fRad;                 //進行方向計算用
    private Vector3 vMovePos;           //移動用
    private float fGrace = 0.025f;      //差
    private bool bStop = false;         //停止フラグ
    //private bool bWayPoint = false;     //中間地点フラグ

    //ベクトル計算用
    public Vector3 vStartVec;
    public Vector3 vEndVec;
    public float fCollisionVec;

    public float fVecRange;

    //距離計算用
    public float fContainer;
    public float fDistance;

    //初回生成時移動許可判定
    public bool bMoveFirst = false;
    private bool bCreateCall = false;



    // Use this for initialization
    void Start () {

        gParentObj = gameObject.transform.parent.gameObject;

        //ラインの座標リストを取得
        lvStorage = gParentObj.GetComponent<line>().lvPointStorage;

        nListCnt = lvStorage.Count;
        nListCntDiv = nListCnt / 2;

        vNewPos = transform.position;

        //どちらのプレス機か
        if (bWallStart == true)
        {
            vLookPos = lvStorage[2];
            transform.LookAt(vLookPos);
            transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
            nNextList = 1;
        }

        if (bWallStart == false)
        {
            vLookPos = lvStorage[nListCnt - 2];
            transform.LookAt(vLookPos);
            transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
            nNextList = nListCnt - 2;
        }

    }

    void Create()
    {
        //gParentObj = gameObject.transform.parent.gameObject;

        //ラインの座標リストを取得
        lvStorage = gParentObj.GetComponent<line>().lvPointStorage;

        nListCnt = lvStorage.Count;
        nListCntDiv = nListCnt / 2;

        vNewPos = transform.position;

        //どちらのプレス機か
        if (bWallStart == true)
        {
            vLookPos = lvStorage[2];
            transform.LookAt(vLookPos);
            transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
            nNextList = 1;
        }

        if (bWallStart == false)
        {
            vLookPos = lvStorage[nListCnt - 2];
            transform.LookAt(vLookPos);
            transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
            nNextList = nListCnt - 2;
        }
    }

    // Update is called once per frame
    void Update () {

        if (bMoveFirst == false)
        {
            bMoveFirst = gParentObj.GetComponent<line>().bPressMove;
            bCreateCall = true;
        }

        if (bCreateCall == true)
        {
            Create();
            bCreateCall = false;
        }

        if (bMoveFirst == true)
        {

            //どちらのプレス機か
            if (bWallStart == true)
            {
                if (/*nNextList <= nListCntDiv &&*/ bStop == false)
                {
                    //移動目標
                    vLookPos = lvStorage[nNextList];

                    //移動
                    fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                    vMovePos = transform.position;

                    vMovePos.x += vSpeed.x * Mathf.Cos(fRad);
                    vMovePos.y += vSpeed.y * Mathf.Sin(fRad);

                    transform.position = vMovePos;

                    transform.LookAt(vLookPos);
                    transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));

                    //目標位置と現在位置の差を確認
                    if (vLookPos.x - fGrace <= transform.position.x && transform.position.x <= vLookPos.x + fGrace &&
                        vLookPos.y - fGrace <= transform.position.y && transform.position.y <= vLookPos.y + fGrace)
                    {
                        nNextList++;
                    }

                    //衝突条件を満たさず最後まで来たら停止
                    if (nNextList == nListCnt)
                    {
                        bStop = true;
                    }
                }

            }
            if (bWallStart == false)
            {
                if (/*nListCntDiv <= nNextList &&*/ bStop == false)
                {
                    //移動目標
                    vLookPos = lvStorage[nNextList];

                    //移動・方向転換
                    fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                    vMovePos = transform.position;

                    vMovePos.x += vSpeed.x * Mathf.Cos(fRad);
                    vMovePos.y += vSpeed.y * Mathf.Sin(fRad);

                    transform.position = vMovePos;

                    transform.LookAt(vLookPos);
                    transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));

                    //目標位置と現在位置の差を確認
                    if (vLookPos.x - fGrace <= transform.position.x && transform.position.x <= vLookPos.x + fGrace &&
                        vLookPos.y - fGrace <= transform.position.y && transform.position.y <= vLookPos.y + fGrace)
                    {
                        nNextList--;
                    }

                    //衝突条件を満たさず最後まで来たら停止
                    if (nNextList == -1)
                    {
                        bStop = true;
                    }
                }
            }

            vOldPos = vNewPos;
            vNewPos = transform.position;
            fContainer = (vNewPos - vOldPos).magnitude;
            fDistance += fContainer;

            //半分のところでマテリアル非表示
            if (gParentObj.GetComponent<line>().fDistanceTotal * 0.5f < fDistance)
            {
                GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }

            //半分とちょっとのところでデストロイ
            if (gParentObj.GetComponent<line>().fDistanceTotal *0.55f < fDistance)
            {
                bStop = true;

                Destroy(gParentObj);
                Destroy(this.gameObject);

                //bWayPoint = true;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (gameObject.tag == "StartPress")
    //    {
    //        //衝突相手が自分の対か
    //        if (collider.gameObject.tag == "EndPress")
    //        {
    //            if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
    //            {
    //                //衝突時角度計算
    //                vStartVec = vLookPos - transform.position;
    //                vEndVec = collider.gameObject.GetComponent<Press>().vLookPos - collider.gameObject.GetComponent<Press>().transform.position;
    //                fCollisionVec = Vector3.Angle(vStartVec, vEndVec);

    //                //一定の角度であればプレス機を止める
    //                if (fCollisionVec > fVecRange && bWayPoint == true)
    //                {
    //                    bStop = true;
    //                    Destroy(gParentObj);
    //                    Destroy(this.gameObject);
    //                }
    //            }
    //        }
    //    }

    //    if (gameObject.tag == "EndPress")
    //    {
    //        //衝突相手が自分の対か
    //        if (collider.gameObject.tag == "StartPress")
    //        {
    //            if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
    //            {
    //                //衝突時角度計算
    //                vEndVec = vLookPos - transform.position;
    //                vStartVec = collider.gameObject.GetComponent<Press>().vLookPos - collider.gameObject.GetComponent<Press>().transform.position;
    //                fCollisionVec = Vector3.Angle(vEndVec, vStartVec);

    //                //一定の角度であればプレス機を止める
    //                if (fCollisionVec > fVecRange && bWayPoint == true)
    //                {
    //                    bStop = true;
    //                    Destroy(gParentObj);
    //                    Destroy(this.gameObject);
    //                }
    //            }
    //        }
    //    }
    //}
}

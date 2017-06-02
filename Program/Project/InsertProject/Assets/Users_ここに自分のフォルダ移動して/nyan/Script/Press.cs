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
    private Vector3 vSpeed = new Vector3(0.04f, 0.04f, 0.04f);
    private float fRad;                 //進行方向計算用
    private Vector3 vMovePos;           //移動用
    private float fGrace = 0.025f;      //差
    public bool bStop = false;          //停止フラグ

    //ベクトル計算用
    public Vector3 vStartVec;
    public Vector3 vEndVec;
    public float fCollisionVec;

    public float fVecRange;

    //距離計算用
    public float fContainer;
    public float fDistance;



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
            vLookPos = lvStorage[0];
            transform.LookAt(vLookPos);
            nNextList = 1;
        }

        if (bWallStart == false)
        {
            vLookPos = lvStorage[nListCnt-1];
            transform.LookAt(vLookPos);
            nNextList = nListCnt - 2;
        }

    }
	
	// Update is called once per frame
	void Update () {

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
                transform.Rotate(new Vector3 (transform.rotation.x, -90.0f, transform.rotation.z));

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

        if (gParentObj.GetComponent<line>().fDistanceTotal / 2 < fDistance)
        {
            bStop = true;

            Destroy(gParentObj);
            Destroy(this.gameObject);
        }

    }

    //private void OnTriggerEnter(Collider collider)
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
    //                if (fCollisionVec > fVecRange)
    //                {
    //                    bStop = true;
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
    //                if (fCollisionVec > fVecRange)
    //                {
    //                    bStop = true;
    //                }
    //            }
    //        }
    //    }
    //}
}

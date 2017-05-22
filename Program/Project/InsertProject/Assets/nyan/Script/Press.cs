using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour {

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
    Vector3 vOldPos;

    //移動関連
    private Vector3 vSpeed = new Vector3(0.05f, 0.05f, 0.05f);
    private float fRad;
    private Vector3 vPos;
    private float fGrace = 0.025f;      //差
    public bool bStop = false;

    //ベクトル計算用
    public Vector3 vStartVec;
    public Vector3 vEndVec;
    public float fVec;


    // Use this for initialization
    void Start () {

        gParentObj = gameObject.transform.parent.gameObject;
        
        //ラインの座標リストを取得
        lvStorage = gParentObj.GetComponent<line>().lvPointStorage;

        nListCnt = lvStorage.Count;
        nListCntDiv = nListCnt / 2;

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

                vPos = transform.position;

                vPos.x += vSpeed.x * Mathf.Cos(fRad);
                vPos.y += vSpeed.y * Mathf.Sin(fRad);

                transform.position = vPos;

                transform.LookAt(vLookPos);

                //目標位置と現在位置の差を確認
                if (vLookPos.x - fGrace <= transform.position.x && transform.position.x <= vLookPos.x + fGrace &&
                    vLookPos.y - fGrace <= transform.position.y && transform.position.y <= vLookPos.y + fGrace)
                {
                    nNextList++;
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

                vPos = transform.position;

                vPos.x += vSpeed.x * Mathf.Cos(fRad);
                vPos.y += vSpeed.y * Mathf.Sin(fRad);

                transform.position = vPos;

                transform.LookAt(vLookPos);

                //目標位置と現在位置の差を確認
                if (vLookPos.x - fGrace <= transform.position.x && transform.position.x <= vLookPos.x + fGrace &&
                    vLookPos.y - fGrace <= transform.position.y && transform.position.y <= vLookPos.y + fGrace)
                {
                    nNextList--;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (gameObject.tag == "StartPress")
        {
            //衝突したのが自分の対なら止める
            if (collider.gameObject.tag == "EndPress")
            {
                if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
                {
                    bStop = true;

                    //衝突時角度計算
                    vStartVec = vLookPos - transform.position;
                    vEndVec = collider.gameObject.GetComponent<Press>().vLookPos - collider.gameObject.GetComponent<Press>().transform.position;
                    fVec = Vector3.Angle(vStartVec, vEndVec);

                }

            }

        }

        if (gameObject.tag == "EndPress")
        {
            //衝突したのが自分の対なら止める
            if (collider.gameObject.tag == "StartPress")
            {
                if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
                {
                    bStop = true;

                    //衝突時角度計算
                    vEndVec = vLookPos - transform.position;
                    vStartVec = collider.gameObject.GetComponent<Press>().vLookPos - collider.gameObject.GetComponent<Press>().transform.position;
                    fVec = Vector3.Angle(vEndVec, vStartVec);

                }
            }

        }
    }
}

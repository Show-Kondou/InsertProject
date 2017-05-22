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
    private float fGrace = 0.025f;
    public bool bStop = false;


    // Use this for initialization
    void Start () {

        gParentObj = gameObject.transform.parent.gameObject;
        
        lvStorage = gParentObj.GetComponent<line>().lvPointStorage;

        nListCnt = lvStorage.Count;
        nListCntDiv = nListCnt / 2;

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

        if (bWallStart == true)
        {
            if (/*nNextList <= nListCntDiv &&*/ bStop == false)
            {

                vLookPos = lvStorage[nNextList];

                fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                vPos = transform.position;

                vPos.x += vSpeed.x * Mathf.Cos(fRad);
                vPos.y += vSpeed.y * Mathf.Sin(fRad);

                transform.position = vPos;

                transform.LookAt(vLookPos);


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

                vLookPos = lvStorage[nNextList];

                fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                vPos = transform.position;

                vPos.x += vSpeed.x * Mathf.Cos(fRad);
                vPos.y += vSpeed.y * Mathf.Sin(fRad);

                transform.position = vPos;

                transform.LookAt(vLookPos);

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
            if (collider.gameObject.tag == "EndPress")
            {
                if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
                {
                    bStop = true;
                }

            }

        }

        if (gameObject.tag == "EndPress")
        {
            if (collider.gameObject.tag == "StartPress")
            {
                if (collider.gameObject.GetComponent<Press>().nPressID == nPressID)
                {
                    bStop = true;
                }
            }

        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (gameObject.tag == "StartPress")
    //    {
    //        if (collision.gameObject.tag == "EndPress")
    //        {
    //            if (collision.gameObject.GetComponent<Press>().nPressID == nPressID)
    //            {
    //                bStop = true;
    //            }
                
    //        }

    //    }

    //    if (gameObject.tag == "EndPress")
    //    {
    //        if (collision.gameObject.tag == "StartPress")
    //        {
    //            if (collision.gameObject.GetComponent<Press>().nPressID == nPressID)
    //            {
    //                bStop = true;
    //            }
    //        }

    //    }

    //}





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour {

    GameObject gParentObj;

    public List<Vector3> lvStorage = new List<Vector3>();
    public int nListCnt;
    public int nListCntDiv;
    public int nNextList;

    Vector3 vLookPos;
    Vector3 vOldPos;

    public bool bWallStart;

    private Vector3 vSpeed = new Vector3(0.05f, 0.05f, 0.05f);
    private float fRad;
    private Vector3 vPos;
    private float fGrace = 0.025f;


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
            if (nNextList <= nListCntDiv)
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
            if (nListCntDiv <= nNextList)
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



        //if (bWallStart == true)
        //{
        //    if (nNextList <= nListCntDiv)
        //    {
        //        vLookPos = lvStorage[nNextList];
        //        vOldPos = transform.position;
        //        transform.LookAt(vLookPos);

        //        transform.position = transform.position + new Vector3(0.01f, 0.01f, 0.0f);

        //        //transform.position = Vector3.Lerp(transform.position, vLookPos,0.5f);

        //        if (transform.position == vLookPos)
        //        {
        //            nNextList++;
        //        }

        //        //vLookPos = lvStorage[nNextList];
        //        //transform.LookAt(vLookPos);
        //        //transform.position = vLookPos;
        //        //nNextList++;
        //    }
        //}

        //if (bWallStart == false)
        //{
        //    if (nListCntDiv <= nNextList)
        //    {
        //        vLookPos = lvStorage[nNextList];
        //        transform.LookAt(vLookPos);
        //        transform.position = vLookPos;
        //        nNextList--;
        //    }
        //}

    }
}

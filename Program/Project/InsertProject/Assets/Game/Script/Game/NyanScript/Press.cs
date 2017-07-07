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
    public float fSpeed = 2.0f;
    public bool bSpeedCheck1 = false;
    public bool bSpeedCheck2 = false;
    public bool bSpeedCheck3 = false;
    private Vector3 vSpeed;
    private float fRad;                 //進行方向計算用
    private Vector3 vMovePos;           //移動用
    private float fGrace = 0.25f;      //差
    private bool bStop = false;         //停止フラグ
    //private bool bWayPoint = false;   //中間地点フラグ

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
    private bool bSummon = false;
    public int nSummonTime;
    private int nSummonCnt = 0;

    //プレス機非表示用フラグ
    public bool bVisible = false;

    //召喚演出プレファブ
    public GameObject gSummonPrefab;

    //ぬるぬる状態判定用
    private GameObject gTouchObj;
    public bool bNulnulS;
    public bool bNulnulE;


    public GameObject DeathParticle;


    // Use this for initialization
    void Start () {
		CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_MAGICWALL, false);
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

            //ぬるぬるを取得
            bNulnulS = gParentObj.GetComponent<line>().bSLineS;

        }

        if (bWallStart == false)
        {
            vLookPos = lvStorage[nListCnt - 2];
            transform.LookAt(vLookPos);
            transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
            nNextList = nListCnt - 2;

            //ぬるぬるを取得
            bNulnulE = gParentObj.GetComponent<line>().bSLineE;

        }

        //召喚演出生成
        GameObject gSummon = Instantiate(gSummonPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z +0.5f), Quaternion.Euler(180, 0, 0)) as GameObject;
        gSummon.transform.parent = transform;

        //CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.MagicWallEmarg, transform.position);

        //生成時にシェーダーを変更

        //スピード初期化
        vSpeed = new Vector3(fSpeed, fSpeed, fSpeed);

    }

    //void Create()
    //{
    //    //gParentObj = gameObject.transform.parent.gameObject;

    //    //ラインの座標リストを取得
    //    lvStorage = gParentObj.GetComponent<line>().lvPointStorage;

    //    nListCnt = lvStorage.Count;
    //    nListCntDiv = nListCnt / 2;

    //    vNewPos = transform.position;

    //    //どちらのプレス機か
    //    if (bWallStart == true)
    //    {
    //        vLookPos = lvStorage[2];
    //        transform.LookAt(vLookPos);
    //        transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
    //        nNextList = 1;
    //    }

    //    if (bWallStart == false)
    //    {
    //        vLookPos = lvStorage[nListCnt - 2];
    //        transform.LookAt(vLookPos);
    //        transform.Rotate(new Vector3(transform.rotation.x, -90.0f, transform.rotation.z));
    //        nNextList = nListCnt - 2;
    //    }
    //}

    // Update is called once per frame
    void Update () {

        if (bMoveFirst == false)
        {
            bSummon = gParentObj.GetComponent<line>().bFirstMove;
        }

        if (bSummon == true)
        {
            nSummonCnt++;

            if (nSummonTime < nSummonCnt)
            {
                bSummon = false;
                bMoveFirst = true;
                
            }
        }

        if (bMoveFirst == true)
        {

            //どちらのプレス機か
            if (bWallStart == true)
            {
				CSoundManager.Instance.PlaySE(AUDIO_LIST.SE_MAGICWALL_MOVE, false);

                //ぬるぬるを取得
                bNulnulS = gParentObj.GetComponent<line>().bSLineS;

                if (/*nNextList <= nListCntDiv &&*/ bStop == false)
                {
                    //移動目標
                    vLookPos = lvStorage[nNextList];

                    //移動
                    fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                    vMovePos = transform.position;

                    vMovePos.x += vSpeed.x * Mathf.Cos(fRad) * Time.deltaTime;
                    vMovePos.y += vSpeed.y * Mathf.Sin(fRad) * Time.deltaTime;

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

                    vOldPos = vNewPos;
                    vNewPos = transform.position;
                    fContainer = (vNewPos - vOldPos).magnitude;
                    fDistance += fContainer;

                }

            }
            if (bWallStart == false)
            {

                //ぬるぬるを取得
                bNulnulE = gParentObj.GetComponent<line>().bSLineE;


                if (/*nListCntDiv <= nNextList &&*/ bStop == false)
                {
                    //移動目標
                    vLookPos = lvStorage[nNextList];

                    //移動・方向転換
                    fRad = Mathf.Atan2(vLookPos.y - transform.position.y, vLookPos.x - transform.position.x);

                    vMovePos = transform.position;

                    vMovePos.x += vSpeed.x * Mathf.Cos(fRad) * Time.deltaTime;
                    vMovePos.y += vSpeed.y * Mathf.Sin(fRad) * Time.deltaTime;

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

                    vOldPos = vNewPos;
                    vNewPos = transform.position;
                    fContainer = (vNewPos - vOldPos).magnitude;
                    fDistance += fContainer;

                }
            }

            //vOldPos = vNewPos;
            //vNewPos = transform.position;
            //fContainer = (vNewPos - vOldPos).magnitude;
            //fDistance += fContainer;

            //スピード更新
            vSpeed = new Vector3(fSpeed, fSpeed, fSpeed);

            //fSpeed += 0.015f;

            if (gParentObj.GetComponent<line>().fDistanceTotal * 0.05f < fDistance && bSpeedCheck1 == false)
            {
                fSpeed += 1.5f;
                bSpeedCheck1 = true;
            }
            if (gParentObj.GetComponent<line>().fDistanceTotal * 0.15f < fDistance && bSpeedCheck2 == false)
            {
                fSpeed += 1.5f;
                bSpeedCheck2 = true;
            }
            if (gParentObj.GetComponent<line>().fDistanceTotal * 0.25f < fDistance && bSpeedCheck3 == false)
            {
                fSpeed += 1.5f;
                bSpeedCheck3 = true;
            }



            //半分のところでマテリアル非表示
            if ( gParentObj.GetComponent<line>().fDistanceTotal * 0.5f < fDistance ) {
				//GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
				if( bWallStart == true && bVisible == false ) {
					bVisible = true;
					line lLine = gParentObj.GetComponent<line>();
					lLine.Visible();
					CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_MAGICWALL_GATTAI );
					// Debug.Log( "gattai" );
				}
				if( bWallStart == false ) {
					bVisible = true;
				}
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
	/// <summary>
	/// トリガーの２D当たり判定
	/// </summary>
	/// <param name="collider">ヒットしたコライダー</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.tag == "StartPress")
        {
            //当たった相手が強スライムなら
            if (collider.gameObject.tag == "Big")
            {
                //ぬるぬる属性をtrue
                gParentObj.GetComponent<line>().bSLineS = true;
                bNulnulS = gParentObj.GetComponent<line>().bSLineS;
                //touchのぬるぬるカウントを最大数に
                gTouchObj = GameObject.Find("Touch");
                gTouchObj.GetComponent<touch>().nSTouchCntS = 2;
                Destroy(collider.gameObject);

                //パーティクル生成
                GameObject par = Instantiate(DeathParticle) as GameObject;
                par.transform.position = gameObject.transform.position;


            }

            //当たった相手がスライムだったら
            if (collider.gameObject.tag == "Enemy" && gParentObj.GetComponent<line>().bSLineS == true ||
                collider.gameObject.tag == "Ally"  && gParentObj.GetComponent<line>().bSLineS == true)
            {
                //当たった相手を子オブジェクトにして座標も弄る
                collider.transform.parent = transform;
                collider.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                collider.GetComponent<CSlimeMove>().m_Sticky = true;
            }
        }

        if (gameObject.tag == "EndPress")
        {
            //当たった相手が強スライムなら
            if (collider.gameObject.tag == "Big")
            {
                //ぬるぬる属性をtrue
                gParentObj.GetComponent<line>().bSLineE = true;
                bNulnulE = gParentObj.GetComponent<line>().bSLineE;
                //touchのぬるぬるカウントを最大数に
                gTouchObj = GameObject.Find("Touch");
                gTouchObj.GetComponent<touch>().nSTouchCntE = 2;
                Destroy(collider.gameObject);

                //パーティクル生成
                GameObject par = Instantiate(DeathParticle) as GameObject;
                par.transform.position = gameObject.transform.position;

            }

            //当たった相手がスライムだったら
            if (collider.gameObject.tag == "Enemy" && gParentObj.GetComponent<line>().bSLineE == true ||
                collider.gameObject.tag == "Ally"  && gParentObj.GetComponent<line>().bSLineE == true)
            {
                //当たった相手を子オブジェクトにして座標も弄る
                collider.transform.parent = transform;
                collider.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                collider.GetComponent<CSlimeMove>().m_Sticky = true;
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

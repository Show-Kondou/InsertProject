using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClearProduction : MonoBehaviour
{
    //定数
    const int MAX_SLIME = 60;           //スライムの数
    const float STOP_POS = 50f;         //スライムが止まる位置

    //変数
    //public bool bStart = false;         //リザルトスタートフラグ
    [SerializeField]
    ResultManager CResultManager;
    [SerializeField]
    GameObject Slime;                   //スライムオブジェクト
    GameObject[] SlimeClone =           //スライムオブジェクト格納用
        new GameObject[MAX_SLIME];
    Vector2 SlimeSize;                  //スライムのサイズ
    int SlimeCnt = 0;                   //スライムの数カウント用

    [SerializeField]
    Canvas CCanvas;                     //キャンバスのサイズ取得用
    Vector2 CanvasSize;                 //キャンバスのサイズ


    Vector3[] TargetVec =               //集合位置への角度
        new Vector3[MAX_SLIME];
    float[] TargetDistance =            //集合位置からスライムまでの距離
        new float[MAX_SLIME];
    float[] MoveDistance =              //集合位置からスライムまでの距離（格納用）
        new float[MAX_SLIME];

    [SerializeField]
    GameObject KingSlime;               //キングスライムオブジェクト
    [SerializeField]
    KingSlimeMove CKingSlimeMove;       //キングスライム移動クラス

    [SerializeField]
    float UntilMoveTime = 1f;           //スライムが移動を始めるまでの時間
    float UntilMoveTimeCnt = 0f;        //スライムが移動を始めるまでの時間カウント
    bool bUntilMoveTime = false;        //スライムが移動を始めるまでの時間開始

    [SerializeField]
    float SlimeIntervalTime = 0.1f;     //スライムが出る間隔の時間
    float SlimeIntervalTimeCnt = 0f;    //スライムが出る間隔の時間カウント

    // Use this for initialization
    void Start()
    {
        KingSlime.SetActive(false);
        CanvasSize = CCanvas.GetComponent<RectTransform>().sizeDelta;
        SlimeSize = Slime.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        CreateSlime();  //スライム生成
        SlimeMove();    //スライム移動処理
    }

    /// <summary>
    /// スライム生成
    /// </summary>
    void CreateSlime()
    {
        if (SlimeCnt >= MAX_SLIME)
        {
            //bStart = false;
            CResultManager.bGameOver = false;
            bUntilMoveTime = true;
        }
        if (CResultManager.bGameOver)
        {
            SlimeIntervalTimeCnt += Time.deltaTime;

            Vector2 Position = new Vector2(             //スライムの出現位置
                Random.Range(-(CanvasSize.x / 2f) + SlimeSize.x / 2f, (CanvasSize.x / 2f) - SlimeSize.x / 2f),
                Random.Range(-(CanvasSize.y / 2f) + SlimeSize.y / 2f, (CanvasSize.y / 2f) - SlimeSize.y / 2f));

            if (SlimeIntervalTimeCnt >= SlimeIntervalTime)
            {
                SlimeClone[SlimeCnt] = Instantiate(Slime, Position, Quaternion.identity);       //スライム生成
                SlimeClone[SlimeCnt].transform.SetParent(CCanvas.transform, false);             //スライムをキャンバスの子に
                TargetDistance[SlimeCnt] = Vector2.Distance(Vector3.zero, SlimeClone[SlimeCnt].transform.localPosition);
                TargetVec[SlimeCnt] = (Vector3.zero - SlimeClone[SlimeCnt].transform.localPosition).normalized;
                SlimeCnt++;
                SlimeIntervalTimeCnt = 0f;
            }
        }

        if (bUntilMoveTime)
            UntilMoveTimeCnt += Time.deltaTime;
    }

    /// <summary>
    /// スライム移動処理
    /// </summary>
    void SlimeMove()
    {
        if (UntilMoveTimeCnt >= UntilMoveTime)
        {
            for (int i = 0; i < MAX_SLIME; i++)
            {
                if (SlimeClone[i] != null)
                    MoveDistance[i] = Vector2.Distance(Vector3.zero, SlimeClone[i].transform.localPosition);

                if (MoveDistance[i] >= STOP_POS)
                    SlimeClone[i].transform.localPosition += TargetVec[i] * 200f * Time.deltaTime;

                if (SlimeClone[i] != null && TargetDistance.Max() <= TargetDistance[i])
                {
                    if (MoveDistance[i] <= STOP_POS)
                    {
                        KingSlime.SetActive(true);
                        for (int j = 0; j < MAX_SLIME; j++)
                            SlimeClone[j].SetActive(false);

                        CKingSlimeMove.Expansion();
                    }
                }
            }
        }
    }
}

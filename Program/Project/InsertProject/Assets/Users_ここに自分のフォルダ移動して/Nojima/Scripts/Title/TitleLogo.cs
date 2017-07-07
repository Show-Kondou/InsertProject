using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    [Header("スタート時の速度"), SerializeField]
    float StartSpeed = 1000f;             //マジックウォールの移動速度
    [Header("閉じるときの速度"), SerializeField]
    float EndSpeed = 1000f;

    //変数

    [SerializeField]
    GameObject[] MagicWall;                 //マジックウォールオブジェクト
    [SerializeField]
    GameObject CCanvasObj;
    float CanvasSizeX;                      //キャンバス横幅

    [SerializeField]
    RectTransform DrawTexture;              //マジックウォールの間から表示される画像

    [SerializeField]
    TitleModelScale CTitleModelScale;

    float WallSize_X = 0f;                  //マジックウォール横幅
    float DrawTextureSize_X = 0f;           //マジックウォールの間から表示される画像の横幅
    float DefaultDrawTextureSize_X = 0f;    //マジックウォールの間から表示される画像のデフォルトのサイズ

    float[] WallPos_X =                     //２つのマジックウォール初期位置
        new float[2] { 0f, 0f };

    bool bShrinking = true;

    //デバッグ用
    public bool bstart = false;                    //マジックウォールの閉じる動作

    // Use this for initialization
    void Start()
    {
        if (CCanvasObj != null)
            CCanvasObj.SetActive(true);
        WallSize_X = GetComponent<RectTransform>().sizeDelta.x; //マジックウォールの横幅取得
        DefaultDrawTextureSize_X = DrawTexture.sizeDelta.x;     //マジックウォールの間から表示される画像の横幅取得
        DrawTexture.sizeDelta =                                 //マジックウォールの間から表示される画像の大きさセット
            new Vector2(DrawTextureSize_X, DrawTexture.sizeDelta.y);
        CanvasSizeX = CCanvasObj.GetComponent<RectTransform>().sizeDelta.x;

        for (int i = 0; i < MagicWall.Length; i++)
            MagicWall[i].SetActive(false);                      //マジックウォールオブジェクトを非アクティブに
    }

    // Update is called once per frame
    void Update()
    {
        WallMoveExpansion();
        if (bstart)
            WallMoveShrinking();
    }

    /// <summary>
    /// テクスチャ拡大処理（マジックウォールの間から表示される画像）
    /// </summary>
    void TextureExpansion()
    {
        //マジックウォールオブジェクトをアクティブに
        for (int i = 0; i < MagicWall.Length; i++)
            MagicWall[i].SetActive(true);

        if (DrawTexture.sizeDelta.x < DefaultDrawTextureSize_X)
        {
            DrawTextureSize_X += (StartSpeed * 2f) * Time.deltaTime;
            DrawTexture.sizeDelta = new Vector2(DrawTextureSize_X, DrawTexture.sizeDelta.y);
        }
    }

    /// <summary>
    /// マジックウォールの拡大時移動
    /// </summary>
    void WallMoveExpansion()
    {
        //マジックウォールが移動できる範囲
        float MoveLimit = (CanvasSizeX / 2f) + (WallSize_X / 2f);

        //一つ目のマジックウォール移動
        if (MagicWall[0].transform.localPosition.x <= MoveLimit)
        {
            WallPos_X[0] += StartSpeed * Time.deltaTime;
            MagicWall[0].transform.localPosition = new Vector2(WallPos_X[0], MagicWall[0].transform.localPosition.y);
        }
        //二つ目のマジックウォール移動
        if (MagicWall[1].transform.localPosition.x >= -MoveLimit)
        {
            WallPos_X[1] -= StartSpeed * Time.deltaTime;
            MagicWall[1].transform.localPosition = new Vector2(WallPos_X[1], MagicWall[1].transform.localPosition.y);
        }

        if (MagicWall[0].transform.localPosition.x >= MoveLimit || MagicWall[1].transform.localPosition.x <= -MoveLimit)
            CTitleModelScale.ModelScale();

        TextureExpansion();
    }


    /// <summary>
    /// テクスチャ縮小処理（マジックウォールの間から表示される画像）
    /// </summary>
    void TextureShrinking()
    {
        if (DrawTexture.sizeDelta.x >= 0f)
        {
            DrawTextureSize_X -= (EndSpeed * 2f) * Time.deltaTime;
            DrawTexture.sizeDelta = new Vector2(DrawTextureSize_X, DrawTexture.sizeDelta.y);
        }
    }

    /// <summary>
    /// マジックウォールの縮小時移動
    /// </summary>
    public void WallMoveShrinking()
    {
        if (bShrinking)
        {

            //一つ目のマジックウォール移動
            if (MagicWall[0].transform.localPosition.x >= 0f)
            {
                WallPos_X[0] -= EndSpeed * Time.deltaTime;
                MagicWall[0].transform.localPosition = new Vector2(WallPos_X[0], MagicWall[0].transform.localPosition.y);
            }
            //二つ目のマジックウォール移動
            if (MagicWall[1].transform.localPosition.x <= 0)
            {
                WallPos_X[1] += EndSpeed * Time.deltaTime;
                MagicWall[1].transform.localPosition = new Vector2(WallPos_X[1], MagicWall[1].transform.localPosition.y);
            }

            if (MagicWall[0].transform.localPosition.x <= DrawTexture.sizeDelta.x / 2f)
                TextureShrinking();

            if (MagicWall[0].transform.localPosition.x <= 0f || MagicWall[1].transform.localPosition.x >= 0f)
            {
                print("スタート");

                for (int i = 0; i < MagicWall.Length; i++)
                    Destroy(MagicWall[i]);  //マジックウォールモデル消す

                //Destroy(TitleLogoModel);
                bShrinking = false;
            }
        }
    }
}

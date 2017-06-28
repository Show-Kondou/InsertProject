using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStart : MonoBehaviour
{

    [SerializeField]
    float M_WALL_SPEED = 1000f;             //マジックウォールの移動速度

    //変数
    [SerializeField]
    GameObject[] MagicWall;                 //マジックウォールオブジェクト

    [SerializeField]
    RectTransform DrawTexture;              //マジックウォールの間から表示される画像

    float WallSize_X = 0f;                  //マジックウォール横幅
    float DrawTextureSize_X = 0f;           //マジックウォールの間から表示される画像の横幅
    float DefaultDrawTextureSize_X = 0f;    //マジックウォールの間から表示される画像のデフォルトのサイズ

    float[] WallPos_X =                     //２つのマジックウォール初期位置
        new float[2] { 0f, 0f };

    [SerializeField]
    ResultManager CResultManager;           //マネージャー

    // Use this for initialization
    void Start()
    {
        WallSize_X = GetComponent<RectTransform>().sizeDelta.x; //マジックウォールの横幅取得
        DefaultDrawTextureSize_X = DrawTexture.sizeDelta.x;     //マジックウォールの間から表示される画像の横幅取得
        DrawTexture.sizeDelta =                                 //マジックウォールの間から表示される画像の大きさセット
            new Vector2(DrawTextureSize_X, DrawTexture.sizeDelta.y);

        for (int i = 0; i < MagicWall.Length; i++)
            MagicWall[i].SetActive(false);                      //マジックウォールオブジェクトを非アクティブに
    }

    // Update is called once per frame
    void Update()
    {
        if (CResultManager.bResultStart || CResultManager.bGameOver)
        {
            TextureExpansion(); //テクスチャ拡大処理
            WallMove();         //マジックウォールの移動処理
        }
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
            DrawTextureSize_X += (M_WALL_SPEED * 2f) * Time.deltaTime;
            DrawTexture.sizeDelta = new Vector2(DrawTextureSize_X, DrawTexture.sizeDelta.y);
        }
    }

    /// <summary>
    /// マジックウォールの移動処理
    /// </summary>
    void WallMove()
    {
        //マジックウォールが移動できる範囲
        float MoveLimit = (CResultManager.GetCanvasSize().x / 2f) + (WallSize_X / 2f);

        //一つ目のマジックウォール移動
        if (MagicWall[0].transform.localPosition.x <= MoveLimit + (-transform.localPosition.x))
        {
            WallPos_X[0] += M_WALL_SPEED * Time.deltaTime;
            MagicWall[0].transform.localPosition = new Vector2(WallPos_X[0], MagicWall[0].transform.localPosition.y);
        }
        //二つ目のマジックウォール移動
        if (MagicWall[1].transform.localPosition.x >= -MoveLimit + (-transform.localPosition.x))
        {
            WallPos_X[1] -= M_WALL_SPEED * Time.deltaTime;
            MagicWall[1].transform.localPosition = new Vector2(WallPos_X[1], MagicWall[1].transform.localPosition.y);
        }
    }
}

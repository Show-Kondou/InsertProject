using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePop : ObjectBase
{

    public GameObject SummonParticle;

    //抽選確率　1/fPopRangeの確率でスライムが出現
    public float fPopRange;
    public float fFailedMinus;
    private float fPopRangeContainer;

    //抽選間隔　指定秒数に一度抽選が行われる
    public float fLotteryTime;

    //タイマーカウント
    private float fTimeCnt;

    // Use this for initialization
    void Start()
    {
        //if (!Application.isEditor)
        //    Destroy(gameObject);    // 本番環境なら削除
        m_OrderNumber = 3;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
        for (int i = 0; i < 10; i++)
        {
            float width = Random.Range(-2.8f, 2.8f);
            float height = Random.Range(-5.0f, 7.0f);
            CSSandwichObjManager.Instance.CreateSandwichObj(0, new Vector2(width, height));
        }

        fPopRangeContainer = fPopRange;
    }

    public override void Execute(float deltaTime)
    {
        //時間をカウント
        fTimeCnt += Time.deltaTime;

        //指定時間が来たら抽選開始
        if (fLotteryTime < fTimeCnt)
        {
            //カウントをリセット
            fTimeCnt = 0;

            //抽選成功
            if (Random.Range(0.0f, fPopRange) <= 1.0f)
            {
                float width = Random.Range(-2.8f, 2.8f);
                float height = Random.Range(-5.0f, 7.0f);
                CSSandwichObjManager.Instance.CreateSandwichObj(CSSandwichObjManager.SandwichObjType.EnemySlime, new Vector2(width, height));

                //パーティクル生成
                GameObject par = Instantiate(SummonParticle) as GameObject;
                par.transform.position = new Vector2(width, height);

                fPopRange = fPopRangeContainer;
            }
            else//抽選失敗
            {
                fPopRange-= fFailedMinus;
            }

        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    float width = Random.Range(-2.0f, 2.0f);
        //    float height = Random.Range(-4.0f, 6.0f);
        //    CSSandwichObjManager.Instance.CreateSandwichObj(CSSandwichObjManager.SandwichObjType.FeverSlime, new Vector2(width, height));
        //}

        //CSSandwichObjManager.Instance.CreateSandwichObj(CSSandwichObjManager.SandwichObjType.BOSS, Vector2.zero);
        //タイプをエネミーにして座標指定

    }

    public override void LateExecute(float deltaTime)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGauge : MonoBehaviour
{
    //定数
    const float ADD_DAMAGE = 2f;//ダメージ量
    protected const float MAX_HP = 1000f;
    const float LimitHP = 100f;

    //変数
    [SerializeField]
    GameObject EnemyGaugeImg;
    RectTransform GaugeTransform;
    Image GaugeImage;
    
    float EnemyHP;

    [SerializeField]
    bool bDamage = false;

    [SerializeField]
    Text HPText;

    
    // Use this for initialization
    void Start()
    {
        GaugeImage = EnemyGaugeImg.GetComponent<Image>();
        GaugeTransform = EnemyGaugeImg.GetComponent<RectTransform>();
        GaugeTransform.sizeDelta = new Vector2(MAX_HP, GaugeTransform.sizeDelta.y);
        //if (MAX_HP > LimitHP)
        //{
        //    float hp = MAX_HP / (MAX_HP * 10f);
        //    print(hp);
        //    GaugeTransform.localScale = new Vector2(hp, GaugeTransform.localScale.y);
        //}


        EnemyHP = GaugeTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        AddDamage();                //ダメージ
        GaugeColor(400f,250f,150f); //ゲージの色
        DrawHP();                   //HPの数値表示

        //倒したとき
        if (EnemyHP <= 0f)
            EnemyHP = MAX_HP;
    }

    void GaugeValue(float t)
    {
        float GaugePos_X = Mathf.Lerp(0f, EnemyHP, t);
        GaugeTransform.sizeDelta = new Vector2(GaugePos_X, GaugeTransform.sizeDelta.y);
    }

    /// <summary>
    /// ダメージ
    /// </summary>
    void AddDamage()
    {
        if (bDamage)
        {
            EnemyHP -= ADD_DAMAGE;
           //bDamage = false;
        }
        GaugeValue(EnemyHP);
    }

    /// <summary>
    /// ゲージの色
    /// </summary>
    /// <param name="max_hp">ゲージ多め</param>
    /// <param name="midium_hp">ゲージ普通</param>
    /// <param name="low_hp">ゲージ少なめ</param>
    void GaugeColor(float max_hp, float midium_hp, float low_hp)
    {
        //ゲージ多め
        if (EnemyHP >= max_hp)
            GaugeImage.color = new Color(0f, 1f, 0f);
        //ゲージ普通
        if (EnemyHP < max_hp && EnemyHP >= midium_hp)
            GaugeImage.color = new Color(1f, 1f, 0f);
        //ゲージ少なめ
        if (EnemyHP <= low_hp)
            GaugeImage.color = new Color(1f, 0f, 0f);
    }

    /// <summary>
    /// テキストにHP表示
    /// </summary>
    void DrawHP()
    {
        float DrawingHP = EnemyHP;// * 100f;
        HPText.text = DrawingHP.ToString("000");    //3桁表示
    }
}

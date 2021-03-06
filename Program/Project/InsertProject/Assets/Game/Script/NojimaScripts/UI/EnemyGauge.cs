﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGauge : MonoBehaviour
{
    //定数
    const float ADD_DAMAGE = 2f;//ダメージ量
    [SerializeField]
    protected float MAX_HP = 0f;
    const float LimitHP = 100f;

    //変数
    [SerializeField]
    GameObject EnemyGaugeImg;
    RectTransform GaugeTransform;
    Image GaugeImage;
    
    float EnemyHP = 0f;

    public bool bDamage = false;

    // Use this for initialization
    void Start()
    {
        //SetHP(MAX_HP);
        GaugeImage = EnemyGaugeImg.GetComponent<Image>();
        GaugeTransform = EnemyGaugeImg.GetComponent<RectTransform>();
        EnemyHP = MAX_HP;

        GaugeTransform.sizeDelta = new Vector2(EnemyHP, GaugeTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        //AddDamage();                //ダメージ
        GaugeColor(MAX_HP * 0.8f, MAX_HP * 0.5f, MAX_HP * 0.2f); //ゲージの色

        //倒したとき
        if (EnemyHP < 0f)
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
    public void AddDamage(float damage)
    {
        //if (bDamage)
        //{
            EnemyHP -= damage;
           //bDamage = false;
        //}
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

    public void SetHP(float HP)
    {
        MAX_HP = HP;
    }
}

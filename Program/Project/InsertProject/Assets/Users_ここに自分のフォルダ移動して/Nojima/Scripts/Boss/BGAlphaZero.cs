using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGAlphaZero : MonoBehaviour
{

    [SerializeField]
    Image[] BossTexture;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < BossTexture.Length; i++)
            BossTexture[i].material.color =
                new Color(BossTexture[i].material.color.r, BossTexture[i].material.color.g, BossTexture[i].material.color.b, 0f);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleModelScale : MonoBehaviour {

    [Header("タイトルモデル"), SerializeField]
    Transform[] TitleModel;

    [Header("拡大スピード"), SerializeField]
    float ScaleSpeed = 2f;

    float ModelSize = 0f;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < TitleModel.Length; i++)
            TitleModel[i].localScale = Vector3.zero;
	}

    /// <summary>
    /// タイトルモデル拡大
    /// </summary>
    public void ModelScale()
    { 
        if (ModelSize <= 1f)
        {
            ModelSize += ScaleSpeed * Time.deltaTime;
            for (int i = 0; i < TitleModel.Length; i++)
                TitleModel[i].localScale = new Vector3(ModelSize, ModelSize, ModelSize);
        }
    }
}

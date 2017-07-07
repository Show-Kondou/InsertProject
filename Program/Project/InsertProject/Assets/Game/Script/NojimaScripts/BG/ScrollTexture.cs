using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour {

    //変数
    [SerializeField]
    Texture[] StageTexture = new Texture[4];
    int StageCnt = 0;
    Renderer rend;

    [SerializeField]
    bool bTerms = false;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = StageTexture[StageCnt];
    }
	
	// Update is called once per frame
	void Update () {
        if (bTerms)
        {
            Terms();
            bTerms = false;
        }

    }


    void Terms()
    {
        StageCnt++;
        rend.material.mainTexture = StageTexture[StageCnt];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTexture : MonoBehaviour {
    
    [SerializeField]
    ResultManager CResultManager;
    [SerializeField]
    Texture[] ResultTextures;
	
	// Update is called once per frame
	void Update () {
		if(!CResultManager.bGameOver)
            GetComponent<Image>().material.mainTexture = ResultTextures[0];
        else
            GetComponent<Image>().material.mainTexture = ResultTextures[1];
	}
}

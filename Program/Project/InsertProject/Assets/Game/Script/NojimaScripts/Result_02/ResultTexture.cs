using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTexture : MonoBehaviour {
    
    [SerializeField]
    ResultManager CResultManager;
    [SerializeField]
    Sprite[] ResultTextures;

	// Update is called once per frame
    void Update()
    {
        if (!CResultManager.bResultMenu)
            GetComponent<Image>().sprite = ResultTextures[0];
        if (CResultManager.bResultMenu || CResultManager.bTimeOverEnd)
            GetComponent<Image>().sprite = ResultTextures[1];
	}
}

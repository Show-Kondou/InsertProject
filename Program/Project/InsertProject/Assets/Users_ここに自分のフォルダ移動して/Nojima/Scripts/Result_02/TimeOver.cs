using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOver : MonoBehaviour {

    [SerializeField]
    GameObject[] MagicWall;
    [SerializeField]
    GameObject TimeOverTexture;
    [SerializeField]
    ResultManager CResultManager;

    float[] MagicWallPOS_X = new float[2]{0f, 0f};

    float TimeOverTextureSize = 0f;
    [SerializeField]
    float WallSpeed = 40f;
    float IntervalTime = 0f;
    float Offset = 10f;

	// Use this for initialization
	void Start () {
        MagicWallPOS_X[0] = TimeOverTexture.GetComponent<RectTransform>().sizeDelta.x / 2f + Offset;
        MagicWallPOS_X[1] = -(TimeOverTexture.GetComponent<RectTransform>().sizeDelta.x / 2f) - Offset;

        TimeOverTextureSize = TimeOverTexture.GetComponent<RectTransform>().sizeDelta.x;

        TimeOverTexture.SetActive(false);
        for (int i = 0; i < 2; i++)
            MagicWall[i].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (CResultManager.bTimeOver)
        {
            IntervalTime += Time.deltaTime;

            TimeOverTexture.SetActive(true);
            for (int i = 0; i < 2; i++)
                MagicWall[i].SetActive(true);

            if (IntervalTime >= 3f)
            {
                MagicWallPOS_X[0] -= WallSpeed * Time.deltaTime;
                MagicWallPOS_X[1] += WallSpeed * Time.deltaTime;
                TimeOverTextureSize -= WallSpeed * 2f * Time.deltaTime;
            }
        }
        if (TimeOverTextureSize <= 0f)
        {
            CResultManager.bTimeOver = false;
            CResultManager.bTimeOverEnd = true;
            TimeOverTexture.SetActive(false);
            for (int i = 0; i < 2; i++)
                MagicWall[i].SetActive(false);
        }

        TimeOverTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(TimeOverTextureSize, TimeOverTexture.GetComponent<RectTransform>().sizeDelta.y);
        for (int i = 0; i < 2; i++)
            MagicWall[i].transform.localPosition = new Vector3(MagicWallPOS_X[i], 0f);
	}
}

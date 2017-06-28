using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

    public bool bResultStart = false;   //リザルトスタート
    public bool bGameOver = false;
    public bool bTimeOver = false;
    [System.NonSerialized]
    public bool bResultMenu = false;
    [System.NonSerialized]
    public bool bTimeOverEnd = false;

    [SerializeField]
    Canvas CCanvas;                     //キャンバスのサイズ取得用
    Vector2 CanvasSize;                 //キャンバスのサイズ取得用

	// Use this for initialization
	void Start () {
        CanvasSize = CCanvas.GetComponent<RectTransform>().sizeDelta;
	}

    public Vector2 GetCanvasSize()
    {
        return CanvasSize;
    }

}

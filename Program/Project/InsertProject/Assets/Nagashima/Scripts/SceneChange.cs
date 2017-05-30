using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ===== シーンチェンジをするスクリプト =====
public class SceneChange : MonoBehaviour 
{
    [SerializeField]
    private string LoadSceneName;

    // ===== シーンチェンジを開始する関数 =====
    public void SceneChangeStart()
    {
        // ボタンを無効化する
        if (this.GetComponent<Button>() != null)
            this.GetComponent<Button>().interactable = false;
       
        // シーンチェンジ
        SceneManager.LoadScene(LoadSceneName);
    }

	// ===== スタート関数 =====
	void Start () 
    {
	}
	
	// ===== 更新関数 =====
	void Update () 
    {	
	}
}

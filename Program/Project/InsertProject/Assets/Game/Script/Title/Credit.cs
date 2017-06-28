/*
 *	▼ File		Credit.cs
 *	
 *	▼ Brief	クレジット表示
 *	
 *	▼ Author	Show Kondou
 *	
 *	▼ Data		First Update	2017年 / 06月 / 02日
 *				Last  Update	2017年 / 06月 / 02日
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *	▼ Class	Credit			
 *	
 *	▼ Brief	クレジット表示
*/
public class Credit : MonoBehaviour {
	[Header("クレジットオープンスプライト"),SerializeField]
	private Sprite  m_OpenSprite;

	[Header("クレジットクローズスプライト"),SerializeField]
	private Sprite  m_CloseSprite;

	[Header("ボタンイメージ"), SerializeField]
	private Image m_ButtonImage;


	[Header("クレジットイメージ"), SerializeField]
	private Image m_CredirImage;

	public bool m_isOpen = false;

	// Use this for initialization
	void Start () {
		Init();
	}

	/*
	 *	▼ Method	Init
	 *	
	 *	▼ Brief	初期化 
	 *	
	 *	▼ In		void 
	 *
	 *	▼ Out		void
	*/
	public void Init() {
		m_isOpen = false;
		var c = m_CredirImage.color;
		c.a = 0.0F;
		m_CredirImage.color = c;
		m_CredirImage.enabled = false;
		m_ButtonImage.sprite = m_OpenSprite;
	}

	/*
 *	▼ Method	TapButton
 *	
 *	▼ Brief	ボタン入力 
 *	
 *	▼ In		
 *
 *	▼ Out		void
*/
	public void TapButton() {
		m_isOpen = !m_isOpen;
		CSoundManager.Instance.PlaySE( AUDIO_LIST.SE_ENTER_1 );
		if( m_isOpen ) {
			m_ButtonImage.sprite = m_CloseSprite;
			var c = m_CredirImage.color;
			c.a = 1.0F;
			m_CredirImage.enabled = true;
			m_CredirImage.color = c;
		} else {
			m_ButtonImage.sprite = m_OpenSprite;
			var c = m_CredirImage.color;
			c.a = 0.0F;
			m_CredirImage.enabled = false;
			m_CredirImage.color = c;
		}
	}

}



/*
 *	▼ End of File
*/
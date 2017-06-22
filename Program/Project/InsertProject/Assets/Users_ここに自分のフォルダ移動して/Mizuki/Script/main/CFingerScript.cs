﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFingerScript : ObjectBase {
	private bool PressButton;   // ボタンを押しているかどうか
	[SerializeField]
	private Image FingerImage;
	private Vector3 MousePos;
	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		Cursor.visible = false;
		PressButton = false;
	}

	public override void Execute(float deltaTime) {
		MousePos = Input.mousePosition;

		// 左クリックで指画像を傾ける
		if(!PressButton && Input.GetMouseButtonDown(0)) {
			PressButton = true;
			FingerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
		}

		// 離したら戻す
		if(PressButton && Input.GetMouseButtonUp(0)) {
			PressButton = false;
			FingerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}

		FingerImage.transform.position = new Vector3(MousePos.x + FingerImage.rectTransform.rect.width * 0.31f,
			MousePos.y + FingerImage.rectTransform.rect.height * 0.43f,0.0f);
	}

	public override void LateExecute(float deltaTime) {

	}
}

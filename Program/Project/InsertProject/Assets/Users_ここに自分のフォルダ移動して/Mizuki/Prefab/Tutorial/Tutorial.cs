using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : ObjectBase {
	[SerializeField]
	private Image FingerImage;
	private Vector3 FingerPos;
	private int Direction;
	[SerializeField]
	private float Speed;
	[SerializeField]
	private float PositionLimit;
	private bool use;
	[SerializeField]
	private float ReVisibleTime;
	private float ReVisibleTimer;
	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		FingerImage.enabled = false;
		use = false;
		ReVisibleTimer = 9999;
	}

	public override void Execute(float deltaTime) {
		if(use) {
			if(Cursor.visible)
				Cursor.visible = false;
			FingerImage.rectTransform.localPosition = new Vector3(FingerPos.x + FingerImage.rectTransform.rect.width * 0.31f,
				FingerImage.rectTransform.rect.height * 0.43f, 0.0f);

			FingerPos.x += Speed * Direction * deltaTime;
			if(FingerPos.x > PositionLimit ) {
				FingerPos.x = PositionLimit;
				Direction = -1;
			}
			if(FingerPos.x < -PositionLimit) {
				FingerPos.x = -PositionLimit;
				Direction = 1;
			}

			if(Input.GetMouseButtonDown(0)) {
				FingerImage.enabled = false;
				use = false;
				ReVisibleTimer = ReVisibleTime;
			}
		}else {
			if(Input.GetMouseButtonDown(0)) {
				ReVisibleTimer = ReVisibleTime;
			}
			ReVisibleTimer -= deltaTime;
			if(ReVisibleTimer < 0) {
				Create();
			}
		}
	}

	public override void LateExecute(float deltaTime) {

	}

	public void Create() {
		Direction = 1;
		Cursor.visible = false;
		FingerImage.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 30));
		FingerImage.enabled = true;
		use = true;
		FingerImage.rectTransform.localPosition = new Vector3(-PositionLimit + FingerImage.rectTransform.rect.width * 0.31f,
				FingerImage.rectTransform.rect.height * 0.43f, 0.0f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBrightness : ObjectBase {
	private int BrightnessDir;	// 明るくするか暗くするか(1or-1)

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		BrightnessDir = -1;

		this.enabled = false;
	}

	public override void Execute(float deltaTime) {

	}

	public override void LateExecute(float deltaTime) {

	}


}

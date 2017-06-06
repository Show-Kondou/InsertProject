using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyMove : CSSandwichObject {
	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {
		base.Execute(deltaTime);

	}

	public override void LateExecute(float deltaTime) {
		base.LateExecute(deltaTime);

	}
}
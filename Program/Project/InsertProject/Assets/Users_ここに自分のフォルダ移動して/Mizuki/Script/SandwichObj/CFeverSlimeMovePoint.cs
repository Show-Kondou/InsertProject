using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFeverSlimeMovePoint : ObjectBase {
	List<GameObject> FeverSlimeMovePoint = new List<GameObject>();

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
	}

	public override void Execute(float deltaTime) {

	}

	public override void LateExecute(float deltaTime) {

	}
}

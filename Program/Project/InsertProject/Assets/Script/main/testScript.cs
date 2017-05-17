using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : ObjectBase {

	// Use this for initialization
	void Start () {
		m_OrderNumber = 3;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {
		transform.position += new Vector3(1,0,0);
        if(Input.anyKeyDown) {
            CSParticleManager.Instance.Play("test");
        }
	}

	public override void LateExecute(float deltaTime) {

	}
}

// 作ったもののテスト動作をしたいときに使うやつ



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : ObjectBase {

	public ParticleSystem testpart;

	// Use this for initialization
	void Start () {
        if(!Application.isEditor)
            Destroy(gameObject);    // 本番環境なら削除
		m_OrderNumber = 3;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		for(int i = 0; i < 5; i++) {
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CSSandwichObjManager.Instance.CreateSandwichObj(0, new Vector2(width, height));
		}
	}

	public override void Execute(float deltaTime) {
		if(SpecialInput.m_bDoubleTap) {
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CSSandwichObjManager.Instance.CreateSandwichObj(0, new Vector2(width, height));
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			float width = Random.Range(-2.0f,2.0f);
			float height = Random.Range(-4.0f,6.0f);
			CSSandwichObjManager.Instance.CreateSandwichObj(CSSandwichObjManager.SandwichObjType.FeverSlime, new Vector2(width, height));
		}
	}

	public override void LateExecute(float deltaTime) {

	}
}

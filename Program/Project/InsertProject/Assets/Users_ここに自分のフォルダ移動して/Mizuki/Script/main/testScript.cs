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

	}

	public override void Execute(float deltaTime) {
        if(Input.anyKeyDown) {
			CSParticleManager.Instance.Play(CSParticleManager.PARTICLE_TYPE.EXPLOSION, new Vector3(0,0,0));
			//var obj = Instantiate(testpart);
			//obj.Play();
		}
	}

	public override void LateExecute(float deltaTime) {

	}
}

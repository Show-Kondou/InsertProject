using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour {

	readonly uint MAX_PARTICLE_NUM = 100;
	[Header("弾けるベクトルの最大値"), SerializeField]
	private Vector3 MAX_VECTOR = new Vector3( 1.0F, 1.0F, 1.0F );


	PartictObj m_ParticleOriginal;
	Vector3 m_TargetPos;

	List<PartictObj> m_ParticleList = new List<PartictObj>();


	// Use this for initialization
	void Start () {
		Init();
		
	}
	
	void Init() {
		for( int j = 0; j < MAX_PARTICLE_NUM; j++ ) {
			var obj = Instantiate( m_ParticleOriginal,transform );
			m_ParticleList.Add( obj );
		}
	}

	void PopObject( Vector3 target, uint popNum = 10 ) {
		for( int i = 0; i < popNum; i++ ) {
			foreach( var j in m_ParticleList ) {
				if( j.IsUse )	return;
				var vec = Vector3.zero;
				vec.x = Random.Range( 0.0F, MAX_VECTOR.x );
				vec.y = Random.Range( 0.0F, MAX_VECTOR.y );
				vec.z = Random.Range( 0.0F, MAX_VECTOR.z );
				j.Pop( vec );
			}
		}
	}
}

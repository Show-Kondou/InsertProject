using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartictObj : MonoBehaviour {

	private Vector3 m_TargetPos;
	private bool m_isUse;

	private Vector3 m_vecMove;
	

	public bool IsUse {
		set { m_isUse = value; }
		get { return m_isUse; }
	}

	// Use this for initialization
	void Start () {
		m_isUse = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( !m_isUse ) return;
		transform.position.Set( m_vecMove.x, m_vecMove.y, m_vecMove.z );
		m_vecMove.x -= 0.1F;
		m_vecMove.y -= 0.1F;
		m_vecMove.z -= 0.1F;
		transform.position = (transform.position - m_TargetPos) / 8;
		//if() {

		//}
	}

	public void Pop( Vector3 vec ) {
		m_vecMove = vec;
		m_isUse = true;
	}
}

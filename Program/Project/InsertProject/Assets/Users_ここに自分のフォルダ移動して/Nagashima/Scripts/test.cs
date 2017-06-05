using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private int testcnt;

    public void testEnemyCount()
    {
        testcnt++;
        GameObject.Find("KingSlime").GetComponent<EnemyCount>().EnemyCnt(testcnt);    
    }

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

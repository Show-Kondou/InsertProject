//====================================
/*
取扱説明書
シングルトンにしたいものに継承させてください。
呼び出しはクラス名.Instans.メソッド()でできます。
 */
//====================================

using UnityEngine;
using System;
using System.Collections;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    protected static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null) {
                Type type = typeof(T);
                instance = (T)FindObjectOfType(type);
                if(instance == null) {

                }
            }
            return instance;
        }
    }
    virtual protected void Awake() {
        if(this != Instance) {
            Destroy(gameObject);
            return;
        }
    }
}

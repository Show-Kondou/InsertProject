using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverGageEffect : MonoBehaviour {
	RectTransform rectTransform = null;

	// Use this for initialization
	void Start () {
	}

	private void Awake() {
	}

	public void SetFirstPosition(Vector3 Position) {
		rectTransform = GetComponent<RectTransform>();
		Debug.Log(Camera.main);
		rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Position);
		Debug.Log("a");
	}
}

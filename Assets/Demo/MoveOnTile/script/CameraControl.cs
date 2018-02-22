using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public bool enableFollow = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enableFollow) {
			UpdateCameraPos ();
		}
	}

	void UpdateCameraPos() {
		float x = transform.position.x;
		float y = transform.position.y;

		Vector3 pos = Camera.main.transform.position;

		pos.x = x;
		pos.y = y;

		Camera.main.transform.position = pos;
	}
}

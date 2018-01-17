using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float speed = 10;

	// Use this for initialization
	void Start () {
		
	}

	void Move(Vector2 moveVec) {

		Vector3 transVec = new Vector3(moveVec.x, moveVec.y, 0f) * speed * Time.deltaTime;
		transform.Translate (transVec);

//		Rigidbody2D body = GetComponent<Rigidbody2D> ();
//		body.AddForce (moveVec * 0.5f);

	}

	void HandleMovement() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			Move (new Vector2 (0, 1));
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			Move (new Vector2(0, -1));
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			Move (new Vector2(-1, 0));
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			Move (new Vector2(1, 0));
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
	
	// Update is called once per frame
	void Update () {
		HandleMovement ();
			
		UpdateCameraPos ();
	}
}

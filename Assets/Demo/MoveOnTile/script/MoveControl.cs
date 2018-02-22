using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour {
	// public setting 
	public float speed = 5.0f;

	public CharacterAnime charAnime;

	// Internal Data 
	protected Rigidbody2D mRigidBody;

	// initialization
	void OnEnable()
	{
		mRigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement ();
	}

	// Movement handling 

	void MoveRigidBody(Vector2 moveDirVec) {
		
		Vector2 moveVec = moveDirVec * speed * Time.deltaTime;
		Vector2 myPos = new Vector2 (transform.position.x, transform.position.y);

		Vector2 newPos = myPos + moveVec;

		mRigidBody.MovePosition (newPos);	
	}

	void HandleMovement() {
		Vector2 moveDirVec = Vector2.zero;

		if (Input.GetKey (KeyCode.UpArrow)) {
			moveDirVec = new Vector2 (0, 1);

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			moveDirVec = new Vector2(0, -1);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			moveDirVec = new Vector2(-1, 0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			moveDirVec = new Vector2(1, 0);
		}

		if (moveDirVec != Vector2.zero) {
			MoveRigidBody (moveDirVec);	
		}

		if (charAnime != null) {
			charAnime.UpdateWithMoveVector (moveDirVec);
		}
	}
}

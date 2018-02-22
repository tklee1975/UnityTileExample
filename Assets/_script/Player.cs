using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float speed = 10;

	public Vector2Int mMoveDir = Vector2Int.zero;

	private bool mIsColliding = false;

	// Use this for initialization
	void Start () {
		
	}

	void Move(Vector2Int moveVec) {

		Vector3 transVec = new Vector3(moveVec.x, moveVec.y, 0f) * speed * Time.deltaTime;
		transform.Translate (transVec);

//		Rigidbody2D body = GetComponent<Rigidbody2D> ();
//		body.AddForce (moveVec * 0.5f);

	}

	void HandleMovement() {
		Vector2Int newMove = Vector2Int.zero;

		if (Input.GetKey (KeyCode.UpArrow)) {
			newMove = new Vector2Int (0, 1);

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			newMove = new Vector2Int(0, -1);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			newMove = new Vector2Int(-1, 0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			newMove = new Vector2Int(1, 0);
		}

		if (mIsColliding && newMove == mMoveDir) {
		}

		if (newMove != mMoveDir) {
			mMoveDir = newMove;
			mIsColliding = false;
		}

		if (mIsColliding == false) {			
			Move (mMoveDir);
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


	Vector2 FindCollisionDiff(Bounds myBox, Bounds box) {
		if (myBox.Intersects (box) == false) {
			return new Vector2 (0, 0);
		}

		Debug.Log ("MoveDir=" + mMoveDir);
		Debug.Log ("myMax:" + myBox.max + " myMin:" + myBox.min);
		Debug.Log ("max:" + box.max + " min:" + box.min);

		float xDiff = 0;
		float yDiff = 0;

		if (mMoveDir.x < 0) {		// Checking the left bound diff
			xDiff = myBox.min.x - box.max.x;		// if collide, value is -ve
		}else if (mMoveDir.x > 0) {		// Checking the left bound diff
			xDiff = myBox.max.x - box.min.x; 		// if collide, value is -ve 
		}else if (mMoveDir.y < 0) {		// Checking the left bound diff
			yDiff = myBox.max.y - box.min.y;		// if collide, value is -ve
		}else if (mMoveDir.y > 0) {		// Checking the left bound diff
			yDiff = myBox.min.y - box.max.y; 		// if collide, value is -ve 
		}


//		float upperDiff = myBox.max.y - box.min.y;		// if collide, value is -ve
//		float lowerDiff = myBox.min.y - box.max.y; 		// if collide, value is -ve 
//
//
//		float yDiff = 0;
//		if (upperDiff > 0) {
//			yDiff = upperDiff;
//		}else if (lowerDiff > 0) {
//			yDiff = lowerDiff;
//		}
//
//
//		float rightDiff = myBox.max.x - box.min.x; 		// if collide, value is -ve 
//
//		float xDiff = 0;
//		if (leftDiff > 0) {
//			xDiff = leftDiff;
//		} else if (rightDiff > 0) {
//			xDiff = rightDiff;
//		}
//
		return new Vector2 (xDiff, yDiff);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("enter trigger. coll=" + coll.gameObject.transform);
		Debug.Log ("enter trigger. overlapPoint= offset=" + coll.offset);

		BoxCollider2D box = coll.gameObject.GetComponent<BoxCollider2D> ();
		BoxCollider2D myBox = gameObject.GetComponent<BoxCollider2D> ();

		mIsColliding = true;

		Vector2 diff = FindCollisionDiff (myBox.bounds, box.bounds);

		Debug.Log ("box:" + box.bounds + " myBox:" + myBox.bounds + " diff=" + diff);

		//  Adjust the game object 
		Vector3 pos = transform.position;
		pos.x -= (diff.x);
		pos.y -= (diff.y);

		Debug.Log ("transform Before=" + transform.position);
		transform.position = pos;
		Debug.Log ("transform after=" + transform.position);
	}

	void OnTriggerExit2D(Collider2D coll) {
		mIsColliding = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("enter collision. coll=" + coll.gameObject.transform);

		BoxCollider2D box = coll.gameObject.GetComponent<BoxCollider2D> ();
		BoxCollider2D myBox = gameObject.GetComponent<BoxCollider2D> ();


		Vector2 diff = FindCollisionDiff (myBox.bounds, box.bounds);

		Debug.Log ("box:" + box.bounds + " myBox:" + myBox.bounds + " diff=" + diff);

		//  Adjust the game object 
		Vector3 pos = transform.position;
		pos.x -= (diff.x);
		//pos.y -= (diff.y);

		transform.position = pos;
	}

	void OnCollisionExit2D(Collision2D coll) {
		Debug.Log ("exit collision. coll=" + coll);
		mIsColliding = false;
	}


	// Update is called once per frame
	void Update () {
		HandleMovement ();

			
		UpdateCameraPos ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnime : MonoBehaviour {
	public enum Direction {
		DOWN = 0,
		UP = 1,
		LEFT = 2,
		RIGHT = 3,
	};



	// internal data 
	private Animator mAnimator;

	private Direction mDir = Direction.DOWN;
	private bool mWalking = false;

	// Use this for initialization
	void Start () {
		mAnimator = GetComponentInChildren<Animator> ();	

		this.Dir = mDir;
		//this.
	}

	public Direction Dir {
		get {
			return mDir;
		}
		set {
			mDir = value;

			mAnimator.SetInteger ("dir", (int)mDir);
		}
	}

	public bool IsWalking {
		get {
			return mWalking;
		}
		set {
			mWalking = value;

			mAnimator.SetBool ("walking", mWalking);
		}
	}

	public void UpdateWithMoveVector(Vector2 moveVec) {
		
		if (moveVec == Vector2.zero) {
			this.IsWalking = false;
			return;
		}

		this.IsWalking = true;
		// X vector has something 
		if (moveVec.x > 0) {
			this.Dir = Direction.RIGHT;
		} else if (moveVec.x < 0) {
			this.Dir = Direction.LEFT;
		}

		// X vector = 0
		if (moveVec.y > 0) {
			this.Dir = Direction.UP;
		} else if (moveVec.y < 0) {
			this.Dir = Direction.DOWN;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimePlayer : MonoBehaviour {
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
		mAnimator = GetComponent<Animator> ();	

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
	
	// Update is called once per frame
	void Update () {
		
	}
}

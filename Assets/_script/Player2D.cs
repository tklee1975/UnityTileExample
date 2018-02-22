using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour {
	protected Rigidbody2D rb2d;
	protected ContactFilter2D contactFilter;		// A struct 
	public float speed = 5.0f;
	protected Vector2 velocity;


	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	protected AnimePlayer mAnimePlayer;		// Animation Player


	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () 
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;

		mAnimePlayer = transform.Find("Body").GetComponent<AnimePlayer> ();
		SetAnime (Vector2.zero);
	}

	
	// Update is called once per frame
	void Update () {
		
	}



	void SetAnime(Vector2 moveVec) {
		if (mAnimePlayer == null) {
			return;
		}

		if (moveVec == Vector2.zero) {
			mAnimePlayer.IsWalking = false;
			return;
		}

		mAnimePlayer.IsWalking = true;
		// X vector has something 
		if (moveVec.x > 0) {
			mAnimePlayer.Dir = AnimePlayer.Direction.RIGHT;
		} else if (moveVec.x < 0) {
			mAnimePlayer.Dir = AnimePlayer.Direction.LEFT;
		}

		// X vector = 0
		if (moveVec.y > 0) {
			mAnimePlayer.Dir = AnimePlayer.Direction.UP;
		} else if (moveVec.y < 0) {
			mAnimePlayer.Dir = AnimePlayer.Direction.DOWN;
		}
	}

	Vector2 GetVelocity() {
		Vector2 moveVec = Vector2.zero;

		if (Input.GetKey (KeyCode.UpArrow)) {
			moveVec = new Vector2 (0, 1);

		} else if (Input.GetKey (KeyCode.DownArrow)) {
			moveVec = new Vector2(0, -1);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			moveVec = new Vector2(-1, 0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			moveVec = new Vector2(1, 0);
		}



		// 
		return speed * moveVec * Time.fixedDeltaTime;

	}

	void FixedUpdate()
	{		
		//velocity.x = targetVelocity.x;
		Vector2 move = GetVelocity();

		Movement (move);

		SetAnime (move);
	}

	void Movement(Vector2 move)
	{

		float distance = move.magnitude;

		if (distance > minMoveDistance) 
		{
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear ();
			for (int i = 0; i < count; i++) {
				hitBufferList.Add (hitBuffer [i]);
			}

			Debug.Log ("Movement: count=" + count + " bufferList=" + hitBufferList);

			for (int i = 0; i < hitBufferList.Count; i++) {
				Vector2 currentNormal = hitBufferList [i].normal;

				float projection = Vector2.Dot (velocity, currentNormal);
				if (projection < 0) 
				{
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}

//			for (int i = 0; i < hitBufferList.Count; i++) 
//			{
//				Vector2 currentNormal = hitBufferList [i].normal;
//				if (currentNormal.y > minGroundNormalY) 
//				{
//					grounded = true;
//					if (yMovement) 
//					{
//						groundNormal = currentNormal;
//						currentNormal.x = 0;
//					}
//				}
//
//				float projection = Vector2.Dot (velocity, currentNormal);
//				if (projection < 0) 
//				{
//					velocity = velocity - projection * currentNormal;
//				}
//
//				float modifiedDistance = hitBufferList [i].distance - shellRadius;
//				distance = modifiedDistance < distance ? modifiedDistance : distance;
//			}
//

		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}

}

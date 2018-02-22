using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

//

[Serializable]
public class SimpleRoadTile : TileBase {
	public Sprite[] spriteList = new Sprite[4];	// 

	// Constant 

	// Constant for Direction 
	const int UP = 1 << 0;
	const int DOWN = 1 << 1;
	const int LEFT = 1 << 2;
	const int RIGHT = 1 << 3;

	// Constant for sprite 
	// note for the sprite
	//		STRAIGHT	 TURN		 3WAY		CROSS
	//		   #		  #			   #		  #
	//         #		  ## 		  ###  	     ###
	//         # 					     	      #
	public const int SPRITE_IDX_STRAIGHT = 0;
	public const int SPRITE_IDX_TURN = 1;
	public const int SPRITE_IDX_3WAY = 2;
	public const int SPRITE_IDX_CROSS = 3;


	public override void RefreshTile(Vector3Int location, ITilemap tilemap)
	{
		// UP, DOWN LEFT, RIGHT


		List<Vector3Int> refreshPosList = new List<Vector3Int> ();

		refreshPosList.Add (location + new Vector3Int ( 1,  0, 0));
		refreshPosList.Add (location + new Vector3Int (-1,  0, 0));
		refreshPosList.Add (location + new Vector3Int ( 0,  0, 0));		// Need to refresh the given location too
		refreshPosList.Add (location + new Vector3Int ( 0,  1, 0));
		refreshPosList.Add (location + new Vector3Int ( 0, -1, 0));


		// Refresh the given position 
		foreach(Vector3Int position in refreshPosList) {
			if (HasRoadTile (tilemap, position)) {
				tilemap.RefreshTile (position);
			}
		}
	}

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		Debug.Log ("GetTileData: location=" + location);

		int flag = HasRoadTile(tileMap, location + new Vector3Int(0, 1, 0)) ? UP : 0;
		flag += HasRoadTile(tileMap, location + new Vector3Int(0, -1, 0)) ? DOWN : 0;
		flag += HasRoadTile(tileMap, location + new Vector3Int(1, 0, 0)) ? RIGHT : 0;
		flag += HasRoadTile(tileMap, location + new Vector3Int(-1, 0, 0)) ? LEFT : 0;

		Vector2Int spriteAndRotation = GetSpriteAndRotation (flag);

		int spriteIndex = spriteAndRotation.x;
		int rotation = spriteAndRotation.y;

		Sprite sprite = spriteList [spriteIndex];
		if (sprite == null) {
			return;
		}

		Matrix4x4 rotationMatrix = GetRotationMatrix (rotation);

		// Update the TileData 
		tileData.transform = rotationMatrix;
		tileData.sprite = sprite;
		tileData.color = Color.white;
		tileData.flags = TileFlags.LockAll;

	}

	private Matrix4x4 GetRotationMatrix(int rotation) {
		Quaternion quaternion = Quaternion.Euler(0f, 0f, -rotation);
		
		Matrix4x4 mat = Matrix4x4.identity;
		mat.SetTRS(Vector3.zero, quaternion, Vector3.one);

		return mat;
	}

	// Return Vector2Int : 
	//		vector.x = Sprite Index 
	//		vector.y = Sprite Rotation 
	//
	//	Note for the Sprite
	//		
	private Vector2Int GetSpriteAndRotation(int mask)
	{
		switch (mask)
		{
		// one connection 
		case UP:
		case DOWN:
			{
				return new Vector2Int (SPRITE_IDX_STRAIGHT, 0);
			}

		case LEFT:
		case RIGHT:	
			{
				return new Vector2Int (SPRITE_IDX_STRAIGHT, 90);
			}

		// two connection (No turn)
		case (UP | DOWN):
			{
				return new Vector2Int (SPRITE_IDX_STRAIGHT, 0);
			}
		case (LEFT | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_STRAIGHT, 90);
			}

		// two connection (turn)
		case (UP | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_TURN, 0);
			}
		case (DOWN | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_TURN, 90);
			}
		case (DOWN | LEFT):
			{
				return new Vector2Int (SPRITE_IDX_TURN, 180);
			}
		case (UP | LEFT):
			{
				return new Vector2Int (SPRITE_IDX_TURN, 270);
			}

		// three connection
		case (UP | LEFT | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_3WAY, 0);
			}

		case (UP | DOWN | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_3WAY, 90);
			}
		case (DOWN | LEFT | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_3WAY, 180);
			}
		case (UP | DOWN | LEFT):
			{
				return new Vector2Int (SPRITE_IDX_3WAY, 270);
			}

		// four connection 
		case (UP | DOWN | LEFT | RIGHT):
			{
				return new Vector2Int (SPRITE_IDX_CROSS, 0);
			}

		default:
			{
				return new Vector2Int (SPRITE_IDX_STRAIGHT, 0);
			}
		}
	}



	// This determines if the Tile at the position is also using RoadTile (same scriptable object).
	private bool HasRoadTile(ITilemap tilemap, Vector3Int position)
	{
		return tilemap.GetTile(position) == this;
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Custom Tile/SimpleRoadTile")]
	public static void CreateTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save SimpleRoadTile",
			"SimpleRoadTile", "asset", "Save SimpleRoadTile", "Assets");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SimpleRoadTile>(), path);
	}
	#endif
}
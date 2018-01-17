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
public class RoadTile : TileBase {
	public Sprite[] roadSpriteList;

	public Sprite insideSprite;
	public Sprite turnSprite1;
	public Sprite turnSprite2;
	public Sprite sideSprite;

	const int TOP = 1 << 0;
	const int DOWN = 1 << 1;
	const int LEFT = 1 << 2;
	const int RIGHT = 1 << 3;


	const int TOP_LEFT = 1 << 0;
	const int TOP_RIGHT= 1 << 1;
	const int DOWN_LEFT = 1 << 2;
	const int DOWN_RIGHT = 1 << 3;

	// This refreshes itself and other RoadTiles that are orthogonally and diagonally adjacent
	public override void RefreshTile(Vector3Int location, ITilemap tilemap)
	{
		for (int yd = -1; yd <= 1; yd++)
			for (int xd = -1; xd <= 1; xd++)
			{
				Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
				if (HasRoadTile (tilemap, position)) {
					tilemap.RefreshTile (position);
				}
			}
	}

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		//Debug.Log ("GetTileData: called. location=" + location);

		// The flag indicate the tile at UP / DOWN / LEFT / RIGHT 
		int mask = HasRoadTile(tileMap, location + new Vector3Int(0, 1, 0)) ? TOP : 0;
		mask += HasRoadTile(tileMap, location + new Vector3Int(0, -1, 0)) ? DOWN : 0;
		mask += HasRoadTile(tileMap, location + new Vector3Int(1, 0, 0)) ? RIGHT : 0;
		mask += HasRoadTile(tileMap, location + new Vector3Int(-1, 0, 0)) ? LEFT : 0;

		// The flag indicate the tile at the four corner 
		int mask2 = HasRoadTile(tileMap, location + new Vector3Int(-1, 1, 0)) ? TOP_LEFT : 0;
		mask2 += HasRoadTile(tileMap, location + new Vector3Int(1, 1, 0)) ? TOP_RIGHT : 0;
		mask2 += HasRoadTile(tileMap, location + new Vector3Int(-1, -1, 0)) ? DOWN_LEFT : 0;
		mask2 += HasRoadTile(tileMap, location + new Vector3Int(1, -1, 0)) ? DOWN_RIGHT : 0;
			
		Sprite sprite = GetSprite(mask, mask2);

		if (sprite == null) {
			return;
		}


		float rotation = GetRotation (mask, mask2);
		Debug.Log ("mask=" + mask + " rotation=" + rotation);
		Quaternion quaternion = Quaternion.Euler(0f, 0f, rotation);

		Matrix4x4 mat = Matrix4x4.identity;
		mat.SetTRS(Vector3.zero, quaternion, Vector3.one);
		tileData.transform = mat;
		tileData.sprite = sprite;
		tileData.color = Color.white;
		tileData.flags = TileFlags.LockTransform;		// need to make the tiled filled with a color 
		//tileData.colliderType = .None;

	}

	private Sprite GetSprite(int mask, int mask2)
	{
		switch (mask)
		{
		case (DOWN | RIGHT):
		case (DOWN | LEFT):
		case (TOP | RIGHT):
		case (TOP | LEFT):
			return turnSprite1;			// the sprite having "L" shape 

		case (TOP | DOWN | RIGHT):
		case (TOP | DOWN | LEFT):
		case (TOP | LEFT | RIGHT):
		case (DOWN | LEFT | RIGHT):
			return sideSprite;			// the sprite having "I" shape 

			
		default:
			break;
		}

		// The sprite of small "-|"
		if ((mask2 & TOP_LEFT) == 0
		    || (mask2 & TOP_RIGHT) == 0
		    || (mask2 & DOWN_LEFT) == 0
		    || (mask2 & DOWN_RIGHT) == 0) {
			return turnSprite2;
		} else {
			return insideSprite;
		}
	}

	// The following determines which rotation to use based on the positions of adjacent RoadTiles
	private float GetRotation(int mask, int mask2)
	{
		switch (mask)
		{
		// The "L" shape
		case (DOWN | RIGHT)	: return 0f;
		case (TOP | RIGHT)	: return 90f;
		case (TOP | LEFT)	: return 180f;	
		case (DOWN | LEFT)	: return 270f;

		// The "I" shape
		case (TOP | DOWN | RIGHT)	: return 0f;
		case (TOP | LEFT | RIGHT)	: return 90f;				
		case (TOP | DOWN | LEFT)	: return 180f;
		case (DOWN | LEFT | RIGHT)	: return 270f;
				

		default				: 
			break;
		}


		if ((mask2 & TOP_LEFT) == 0) {
			return 0;
		}

		if ((mask2 & TOP_RIGHT) == 0) {
			return 270;
		}

		if ((mask2 & DOWN_LEFT) == 0) {
			return 90;
		}

		if ((mask2 & DOWN_RIGHT) == 0) {
			return 180;
		}
	
		return 0;
	}

	// This determines if the Tile at the position is also using RoadTile (same scriptable object).
	private bool HasRoadTile(ITilemap tilemap, Vector3Int position)
	{
		return tilemap.GetTile(position) == this;
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Custom Tile/RoadTile")]
	public static void CreateTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save RoadTile",
			"RoadTile", "asset", "Save Tile", "Assets");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RoadTile>(), path);
	}
	#endif
}

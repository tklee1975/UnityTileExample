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
public class TransformTile : TileBase {
	public Sprite customSprite;	// Create a square sprite asset for it
	public Color color = Color.white;
	public float rotateDegree = 0;

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		// Set the Matrix4x4 using Quaternion and SetTRS 
		Quaternion quaternion = Quaternion.Euler(0f, 0f, -rotateDegree);	// ken: rotate using Z-axis
																			// degree > 0: clockwise 

		Matrix4x4 mat = Matrix4x4.identity;
		mat.SetTRS(Vector3.zero, quaternion, Vector3.one);

		// Define the new Matrix
		tileData.transform = mat;


		// Define the sprite, the image to be rotated
		tileData.sprite = customSprite;				// manadatory to show something 

		// using default Setting 
		tileData.color = color;				
		tileData.colliderType = Tile.ColliderType.None;

		// Confirm the change 
		tileData.flags = TileFlags.LockAll;		// need to make the tiled filled with a color 
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Custom Tile/TransformTile")]
	public static void CreateTransformTile()
	{

		string path = EditorUtility.SaveFilePanelInProject("Save TransformTile",
			"TransformTile", "asset", "Save TransformTile", "tileAsset");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TransformTile>(), path);
	}
	#endif
}

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

//

//public virtual Color GetColor (Vector3Int position);
//public virtual Sprite GetSprite (Vector3Int position);
//public virtual TileFlags GetTileFlags (Vector3Int position);
//public virtual Matrix4x4 GetTransformMatrix (Vector3Int position);


[Serializable]
public class ColorTile : TileBase {
	public Color customColor = Color.black;
	public Sprite customSprite;	// Create a square sprite asset for it
	public bool isPassable = true;


	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		//tileData.transform = Matrix4x4.identity;
		tileData.color = customColor;				// the main input 
		tileData.sprite = customSprite;				// manadatory to show something 
		tileData.colliderType = isPassable ? Tile.ColliderType.None : Tile.ColliderType.Grid;
		tileData.flags = TileFlags.LockAll;		// need to make the tiled filled with a color 

		//Debug.Log ("TileMap: " + InfoTileMap (tileMap));
		//Debug.Log ("  Tile: " + InfoTile (location, tileMap, tileData));
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Custom Tile/Color Tile")]
	public static void CreateTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save ColorTile",
						"ColorTile", "asset", "Save ColorTile", "Assets");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ColorTile>(), path);
	}
	#endif
}

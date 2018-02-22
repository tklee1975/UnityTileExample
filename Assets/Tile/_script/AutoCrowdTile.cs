using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

// CrowdTile: 
public class AutoCrowdTile : TileBase {
	public Sprite[] spriteList;

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		//tileData.transform = Matrix4x4.identity;
//		tileData.color = customColor;				// the main input 
//		tileData.sprite = customSprite;				// manadatory to show something 
//		tileData.colliderType = isPassable ? Tile.ColliderType.None : Tile.ColliderType.Grid;
//		tileData.flags = TileFlags.LockAll;		// need to make the tiled filled with a color 

		//Debug.Log ("TileMap: " + InfoTileMap (tileMap));
		//Debug.Log ("  Tile: " + InfoTile (location, tileMap, tileData));
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Custom Tile/AutoCrowdTile")]
	public static void CreateTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save AutoCrowdTile",
							"AutoCrowdTile", "asset", "Save AutoCrowdTile", "Assets");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AutoCrowdTile>(), path);
	}
	#endif

}

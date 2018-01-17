using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class RandomTile : TileBase {
	public Sprite[] spriteList;

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		//tileData.transform = tileMap.GetTransformMatrix (location);	// Matrix4x4.identity;
		int index = Random.Range (0, spriteList.Length);
		Sprite selectedTile = spriteList [index];

		tileData.sprite = selectedTile;

		tileData.flags = TileFlags.LockAll;
		//SetTRS (transform, Quaternion.identity, Vector3.Normalize);
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Custom Tile/RandomTile")]
	public static void CreateTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save RandomTile",
			"RandomTile", "asset", "Save Tile", "Assets");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomTile>(), path);
	}
	#endif
}

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
public class AnimeTile : TileBase {
	public Sprite[] spriteList;
	public float animationSpeed = 1.0f;
	public float startTime = 1.0f; 

	protected Sprite GetFirstSprite() {
		if (spriteList == null || spriteList.Length == 0) {
			return null;
		}

		return spriteList [0];
	}

	public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
	{
		// Define the new Matrix
		tileData.transform = Matrix4x4.identity;;

		tileData.sprite = GetFirstSprite();

		// using default Setting 
		tileData.color = Color.white;				
		tileData.colliderType = Tile.ColliderType.None;

		// Confirm the change 
		tileData.flags = TileFlags.LockAll;		// need to make the tiled filled with a color 
	}

	public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData)
	{
		if (spriteList.Length == 0) {
			return false;
		}

		tileAnimationData.animatedSprites = spriteList;
		tileAnimationData.animationSpeed = animationSpeed;
		tileAnimationData.animationStartTime = startTime;
		return true;
	}


	#if UNITY_EDITOR
	[MenuItem("Assets/Custom Tile/AnimeTile")]
	public static void CreateAnimeTile()
	{

		string path = EditorUtility.SaveFilePanelInProject("Save AnimeTile",
			"AnimeTile", "asset", "Save AnimeTile", "tileAsset");
		if (path == "") {
			return;
		}

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AnimeTile>(), path);
	}
	#endif
}

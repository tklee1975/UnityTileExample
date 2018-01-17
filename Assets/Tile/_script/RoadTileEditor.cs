using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(RoadTile))]
public class RoadTileEditor : Editor
{
	private RoadTile tile { get { return (target as RoadTile); } }

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();


		// 
		tile.insideSprite = (Sprite) EditorGUILayout.ObjectField("Inside", tile.insideSprite, typeof(Sprite), false, null);
		tile.sideSprite = (Sprite) EditorGUILayout.ObjectField("Side", tile.sideSprite, typeof(Sprite), false, null);
		tile.turnSprite1 = (Sprite) EditorGUILayout.ObjectField("Turn 1", tile.turnSprite1, typeof(Sprite), false, null);
		tile.turnSprite2 = (Sprite) EditorGUILayout.ObjectField("Turn 2", tile.turnSprite2, typeof(Sprite), false, null);

		if (EditorGUI.EndChangeCheck ()) {
			EditorUtility.SetDirty (tile);
		}
	}
}
#endif


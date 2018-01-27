﻿using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(SimpleRoadTile))]
public class SimpleRoadTileEditor : Editor
{
	private SimpleRoadTile tile { get { return (target as SimpleRoadTile); } }


	public void OnEnable()
	{
		if (tile.spriteList == null || tile.spriteList.Length != 4)
		{
			tile.spriteList = new Sprite[4];
			EditorUtility.SetDirty(tile);
		}
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();


		int idx;
		string name;

		// 
		idx = SimpleRoadTile.SPRITE_IDX_STRAIGHT;
		name = "Straight";
		tile.spriteList[idx] = (Sprite) EditorGUILayout.ObjectField(name, tile.spriteList[idx], typeof(Sprite), false, null);

		idx = SimpleRoadTile.SPRITE_IDX_TURN;
		name = "Turn";
		tile.spriteList[idx] = (Sprite) EditorGUILayout.ObjectField(name, tile.spriteList[idx], typeof(Sprite), false, null);

		idx = SimpleRoadTile.SPRITE_IDX_3WAY;
		name = "3Ways";
		tile.spriteList[idx] = (Sprite) EditorGUILayout.ObjectField(name, tile.spriteList[idx], typeof(Sprite), false, null);


		idx = SimpleRoadTile.SPRITE_IDX_CROSS;
		name = "Cross";
		tile.spriteList[idx] = (Sprite) EditorGUILayout.ObjectField(name, tile.spriteList[idx], typeof(Sprite), false, null);


		if (EditorGUI.EndChangeCheck ()) {
			EditorUtility.SetDirty (tile);
		}
	}
}
#endif


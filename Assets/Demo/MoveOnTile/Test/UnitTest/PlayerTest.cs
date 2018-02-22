using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class PlayerTest : BaseTest {
	public AnimePlayer testAnimePlayer;


	[Test]
	public void ChangeDir()
	{
		AnimePlayer.Direction curDir = testAnimePlayer.Dir;
		AnimePlayer.Direction newDir = (AnimePlayer.Direction)(((int) curDir + 1) % 4);

		testAnimePlayer.Dir = newDir;
	}

	[Test]
	public void ToggleWalk()
	{
		bool newFlag = ! testAnimePlayer.IsWalking;

		testAnimePlayer.IsWalking = newFlag;
	}
}

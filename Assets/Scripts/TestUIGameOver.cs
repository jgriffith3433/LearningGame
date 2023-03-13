using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TestUIGameOver : State
{
	void OnEnable()
	{
		GameManager.Instance.GameOver();
		ChangeState("None");
	}

	void OnDisable()
	{
	}
}

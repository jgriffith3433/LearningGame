using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TestHideDialogue : State
{
	[SerializeField] private float m_HideDelay = 1f;

	void OnEnable()
	{
		GameManager.Instance.DialogueController.HideDialogueWithDelay(m_HideDelay);
	}

	void OnDisable()
	{
	}
}

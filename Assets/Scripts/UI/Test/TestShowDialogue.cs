using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TestShowDialogue : State
{
	[SerializeField] private string m_DialogText = "";

	void OnEnable()
	{
		GameManager.Instance.DialogueController.ShowDialogueText(m_DialogText);
	}

	void OnDisable()
	{
	}
}

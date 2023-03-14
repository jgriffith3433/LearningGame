using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TestShowDialogueLocalized : State
{
	[SerializeField] private string m_DialogLocalizationKey = "";

	void OnEnable()
	{
		GameManager.Instance.DialogueController.ShowDialogueTranslatedText(m_DialogLocalizationKey);
	}

	void OnDisable()
	{
	}
}

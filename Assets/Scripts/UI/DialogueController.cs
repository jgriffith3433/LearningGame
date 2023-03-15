using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Gamekit3D;

public class DialogueController : StateMachine
{
    [SerializeField] private ShowDialogueState m_ShowDialogueState = null;
    [SerializeField] private HideDialogueState m_HideDialogueState = null;

    public void ShowDialogueText(string text)
    {
        m_ShowDialogueState.SetDialogueText(text);
        ChangeState("ShowDialogue");
    }

    public void ShowDialogueTranslatedText(string phraseKey)
    {
        m_ShowDialogueState.SetDialogueText(LocalizationManager.Instance[phraseKey]);
        ChangeState("ShowDialogue");
    }

    public void HideDialogueWithDelay(float delay = 0)
    {
        m_HideDialogueState.SetDelay(delay);
        ChangeState("HideDialogue");
    }
}

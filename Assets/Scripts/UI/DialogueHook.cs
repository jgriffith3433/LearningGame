using UnityEngine;

public class DialogueHook : MonoBehaviour
{
    public void ShowDialogueText(string text)
    {
        GameManager.Instance.DialogueController.ShowDialogueText(text);
    }

    public void ShowDialogueTranslatedText(string phraseKey)
    {
        GameManager.Instance.DialogueController.ShowDialogueTranslatedText(phraseKey);
    }

    public void HideDialogueWithDelay(float delay = 0)
    {
        GameManager.Instance.DialogueController.HideDialogueWithDelay(delay);
    }
}

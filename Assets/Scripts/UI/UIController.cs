using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Gamekit3D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : StateMachine
{
    [SerializeField] private float m_FadeDuration = 1f;
    [SerializeField] private CanvasGroup m_FaderCanvasGroup = null;
    [SerializeField] private TMPro.TMP_Text m_LevelSelectionText = null;
    [SerializeField] private Button m_PlayButton = null;
    private bool m_IsFading = false;

    public bool IsFading
    {
        get
        {
            return m_IsFading;
        }
    }

    public void OnClickResume()
    {
        GameManager.Instance.Resume();
    }

    public void ChangeLevelSelectionText(string text)
    {
        m_LevelSelectionText.text = text;
        if (text == "Select Level")
        { //hide play button
            m_PlayButton.gameObject.SetActive(false);

        }
        else {//show play button
            m_PlayButton.gameObject.SetActive(true);
        }
    }
    public void OnClickOptionsButton()
    {
        ChangeState("Options");
    }

    public void OnClickControlsButton()
    {
        ChangeState("Controls");
    }

    public void OnClickAudioButton()
    {
        ChangeState("Audio");
    }

    public void OnClickAudioBackButton()
    {
        ChangeState("Options");
    }

    public void OnClickControlsBackButton()
    {
        ChangeState("Options");
    }

    public void OnClickCloseButton()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            ChangeState("Title");
        }
        else
        {
            GameManager.Instance.Resume();
        }
    }

    public void OnClickPlay()
    {
        GameManager.Instance.LoadLevel(m_LevelSelectionText.text);
    }

    public void OnClickQuitButton()
    {
        GameManager.Instance.Quit();
    }

    protected IEnumerator FadeCanvasGroup(float finalAlpha, CanvasGroup canvasGroup)
    {
        m_IsFading = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / m_FadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
        m_IsFading = false;
    }

    public IEnumerator FadeState(string newState)
    {
        m_FaderCanvasGroup.alpha = 0f;
        m_FaderCanvasGroup.gameObject.SetActive(true);
        yield return FadeCanvasGroup(1f, m_FaderCanvasGroup);

        ChangeState(newState);

        m_FaderCanvasGroup.alpha = 1f;
        yield return FadeCanvasGroup(0f, m_FaderCanvasGroup);
        m_FaderCanvasGroup.gameObject.SetActive(false);
    }
}

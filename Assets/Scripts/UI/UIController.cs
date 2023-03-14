using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Gamekit3D;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : StateMachine
{
    public void OnClickPlay()
    {
        GameManager.Instance.LoadLevel();
    }

    public void OnClickResume()
    {
        GameManager.Instance.Resume();
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

    public void OnClickQuitButton()
    {
        GameManager.Instance.Quit();
    }
}

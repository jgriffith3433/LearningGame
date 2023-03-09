using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class UI : StateMachine
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnClickPlay()
    {
        ChangeStateToPlay();
        GameManager.Instance.Play();
    }

    public void OnClickResume()
    {
        ChangeStateToPlay();
    }

    private void ChangeStateToPlay()
    {
        ChangeState("Level_" + GameManager.Instance.CurrentLevelNumber);
        GameManager.Instance.Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.Paused)
            {
                ChangeStateToPlay();
            }
            else
            {
                ChangeState("Pause");
                GameManager.Instance.Pause();
            }
        }
    }
}

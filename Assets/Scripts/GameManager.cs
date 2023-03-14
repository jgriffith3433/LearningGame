using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;
using Gamekit3D;
using UnityEngine.Playables;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIController m_UIController = null;
    [SerializeField] private DialogueController m_DialogueController = null;
    [SerializeField] private float m_SecondsToWaitGameOver = 2f;
    [SerializeField] private float m_SecondsToWaitStartGame = 2f;

    private bool m_AlwaysDisplayMouse = true;

    public bool Paused = false;
    public int CurrentLevelNumber = 1;

    public UIController UIController
    {
        get
        {
            return m_UIController;
        }
    }

    public DialogueController DialogueController
    {
        get
        {
            return m_DialogueController;
        }
    }

    private void Start()
    {
        HandleMouseDisplayAndLock();
        StartCoroutine(GoToTitleScreen(m_SecondsToWaitStartGame));
    }

    private void HandleMouseDisplayAndLock()
    {
        if (!m_AlwaysDisplayMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LoadLevel()
    {
        m_AlwaysDisplayMouse = false;
        HandleMouseDisplayAndLock();
        SceneManager.LoadScene("Level_" + CurrentLevelNumber.ToString());
        m_UIController.ChangeState("Play");
    }

    public void GameOver()
    {
        m_UIController.ChangeState("GameOver");
        StartCoroutine(GoToTitleScreen(m_SecondsToWaitGameOver));
    }

    public void Resume()
    {
        SwitchPauseState();
    }

    public void Pause()
    {
        SwitchPauseState();
    }

    private IEnumerator GoToTitleScreen(float sec)
    {
        yield return new WaitForSeconds(sec);
        m_AlwaysDisplayMouse = true;
        HandleMouseDisplayAndLock();
        SceneManager.LoadScene("Title");
        m_UIController.ChangeState("Title");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                m_UIController.ChangeState("Title");
            }
            else
            {
                if (Paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    protected void SwitchPauseState()
    {
        if (Paused && Time.timeScale > 0 || !Paused && ScreenFader.IsFading)
            return;

        if (!m_AlwaysDisplayMouse)
        {
            Cursor.lockState = Paused ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !Paused;
        }

        for (int i = 0; i < LevelManager.Instance.Directors.Length; i++)
        {
            if (LevelManager.Instance.Directors[i].state == PlayState.Playing && !Paused)
            {
                LevelManager.Instance.Directors[i].Pause();
            }
            else if (LevelManager.Instance.Directors[i].state == PlayState.Paused && Paused)
            {
                LevelManager.Instance.Directors[i].Resume();
            }
        }

        if (!Paused)
            CameraShake.Stop();
        if (PlayerInput.Instance != null)
        {
            if (Paused)
                PlayerInput.Instance.GainControl();
            else
                PlayerInput.Instance.ReleaseControl();
        }

        if (Paused)
            m_UIController.ChangeState("Play");
        else
            m_UIController.ChangeState("Pause");

        Time.timeScale = Paused ? 1 : 0;

        Paused = !Paused;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}

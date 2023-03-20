using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;
using Gamekit3D;
using UnityEngine.Playables;
using UnityEditor;
using System;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIController m_UIController = null;
    [SerializeField] private DialogueController m_DialogueController = null;
    [SerializeField] private float m_SecondsToWaitGameOver = 2f;
    [SerializeField] private float m_SecondsToWaitStartGame = 2f;

    private bool m_AlwaysDisplayMouse = true;
    private bool m_Transitioning = false;

    public bool Paused = false;

    public Scene CurrentScene
    {
        get
        {
            return LevelManager.Instance.gameObject.scene;
        }
    }

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

    public bool Transitioning
    {
        get
        {
            return m_Transitioning;
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

    public void LoadLevel(string levelName)
    {
        if (!m_Transitioning)
        {
            m_AlwaysDisplayMouse = true;
            HandleMouseDisplayAndLock();
            StartCoroutine(Transition(levelName, SceneTransitionDestination.DestinationTag.A, "Play"));
        }
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
        yield return Transition("Title", SceneTransitionDestination.DestinationTag.A, "Title", TransitionPoint.TransitionType.DifferentNonGameplayScene);
    }

    public void RestartScene(bool resetHealth = true)
    {
        StartCoroutine(Transition(CurrentScene.name, LevelManager.Instance.ZoneRestartDestinationTag, "Play"));
    }

    public void RestartSceneWithDelay(float delay, bool resetHealth = true)
    {
        StartCoroutine(CallWithDelay(delay, RestartScene, resetHealth));
    }

    static IEnumerator CallWithDelay<T>(float delay, Action<T> call, T parameter)
    {
        yield return new WaitForSeconds(delay);
        call(parameter);
    }

    public void TransitionToScene(TransitionPoint transitionPoint, string uiStateName)
    {
        StartCoroutine(Transition(transitionPoint.newSceneName, transitionPoint.transitionDestinationTag, uiStateName, transitionPoint.transitionType));
    }

    private IEnumerator Transition(string newSceneName, SceneTransitionDestination.DestinationTag destinationTag, string uiStateName, TransitionPoint.TransitionType transitionType = TransitionPoint.TransitionType.DifferentScene)
    {
        m_Transitioning = true;
        PersistentDataManager.SaveAllData();

        if (InputManager.Instance)
            InputManager.Instance.ReleaseControl();

        yield return m_UIController.FadeState("Loading");
        PersistentDataManager.ClearPersisters();
        yield return new WaitForSeconds(1f);
        yield return SceneManager.LoadSceneAsync(newSceneName);
        if (InputManager.Instance)
            InputManager.Instance.ReleaseControl();

        PersistentDataManager.LoadAllData();
        SceneTransitionDestination entrance = LevelManager.Instance.GetDestination(destinationTag);
        LevelManager.Instance.MoveToTransitionDestination(entrance);
        yield return m_UIController.FadeState(uiStateName);
        if (InputManager.Instance)
            InputManager.Instance.GainControl();

        m_Transitioning = false;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentScene.name == "Title")
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
        if (Paused && Time.timeScale > 0 || !Paused && m_UIController.IsFading)
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
        if (InputManager.Instance != null)
        {
            if (Paused)
                InputManager.Instance.GainControl();
            else
                InputManager.Instance.ReleaseControl();
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

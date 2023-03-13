using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UI m_UI = null;
    [SerializeField] private float m_SecondsToWaitGameOver = 2f;
    [SerializeField] private float m_SecondsToWaitStartGame = 2f;

    public bool Paused = false;
    public int CurrentLevelNumber = 1;

    private void Start()
    {
        StartCoroutine(GoToTitleScreen(m_SecondsToWaitStartGame));
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_" + CurrentLevelNumber.ToString());
    }

    public void GameOver()
    {
        m_UI.ChangeState("GameOver");
        StartCoroutine(GoToTitleScreen(m_SecondsToWaitGameOver));
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Paused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
    }

    private IEnumerator GoToTitleScreen(float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene("Title");
        m_UI.ChangeState("Title");
    }
}

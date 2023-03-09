using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pixelplacement;

public class GameManager : Singleton<GameManager>
{
    public bool Paused = false;
    public int CurrentLevelNumber = 1;

    public void Play()
    {
        SceneManager.LoadScene("Level_" + CurrentLevelNumber.ToString());
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
}

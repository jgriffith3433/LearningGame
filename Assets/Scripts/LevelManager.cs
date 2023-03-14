using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.Playables;

public class LevelManager : Singleton<LevelManager>
{
    private PlayableDirector[] m_Directors = null;

    public PlayableDirector[] Directors
    {
        get
        {
            return m_Directors;
        }
    }

    private void Start()
    {
        m_Directors = GetComponents<PlayableDirector>();
    }
}

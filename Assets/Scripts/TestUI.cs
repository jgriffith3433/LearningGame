using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class TestUI : StateMachine
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

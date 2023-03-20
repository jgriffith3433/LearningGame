using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Gamekit3D;
using UnityEngine.SceneManagement;

public class StateMachineHook : MonoBehaviour
{
    private StateMachine m_StateMachine = null;

    private void Awake()
    {
        m_StateMachine = GetComponent<StateMachine>();
    }

    public void ChangeState(string state)
    {
        m_StateMachine.ChangeState(state);
    }
}

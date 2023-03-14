using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGSetActive : MonoBehaviour
{
    public enum WhenToActivate
    {
        Awake,
        Start,
        Enable,
        Disable
    }

    [SerializeField] private WhenToActivate m_WhenToActivate = WhenToActivate.Enable;
    [SerializeField] private GameObject[] m_GameObjects = null;


    private void Awake()
    {
        if (m_WhenToActivate == WhenToActivate.Awake)
        {
            ActivateAll();
        }
    }

    private void Start()
    {
        if (m_WhenToActivate == WhenToActivate.Start)
        {
            ActivateAll();
        }
    }

    private void OnEnable()
    {
        if (m_WhenToActivate == WhenToActivate.Enable)
        {
            ActivateAll();
        }
    }

    private void OnDisable()
    {
        if (m_WhenToActivate == WhenToActivate.Disable)
        {
            ActivateAll();
        }
    }

    private void ActivateAll()
    {
        foreach (var g in m_GameObjects)
        {
            g.SetActive(true);
        }
    }
}

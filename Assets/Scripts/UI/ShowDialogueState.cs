using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pixelplacement;

namespace Gamekit3D
{
    public class ShowDialogueState : State
    {
        [SerializeField] private Animator m_Animator = null;
        [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI = null;

        private readonly int m_HashActive = Animator.StringToHash("Active");

        public void SetDialogueText(string text)
        {
            m_TextMeshProUGUI.text = text;
        }

        private void OnEnable()
        {
            m_Animator.SetBool(m_HashActive, true);
        }

        private void OnDisable()
        {
            m_Animator.SetBool(m_HashActive, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Gamekit3D
{
    public class HideDialogueState : State
    {
        [SerializeField] private Animator m_Animator = null;

        private float m_Delay = 0;
        private readonly int m_HashActive = Animator.StringToHash("Active");
        private Coroutine m_HideDialogueCoroutine = null;

        public void SetDelay(float delay)
        {
            m_Delay = delay;
        }

        private void OnEnable()
        {
            if (m_HideDialogueCoroutine != null)
            {
                StopCoroutine(m_HideDialogueCoroutine);
                m_HideDialogueCoroutine = null;
            }
            if (m_Delay == 0)
            {
                m_Animator.SetBool(m_HashActive, false);
            }
            else
            {
                m_HideDialogueCoroutine = StartCoroutine(HideDialogue(m_Delay));
            }
        }

        private IEnumerator HideDialogue(float sec)
        {
            yield return new WaitForSeconds(sec);
            m_Animator.SetBool(m_HashActive, false);
            m_HideDialogueCoroutine = null;
        }
    }
}

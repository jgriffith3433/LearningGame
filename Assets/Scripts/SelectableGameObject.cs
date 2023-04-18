using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SelectableGameObject : MonoBehaviour
{
    [SerializeField] Renderer m_SelectableRendererComponent = null;
    [SerializeField] TMP_Text m_SelectableTextMeshPro = null;
    [SerializeField] Color m_SelectedColor = Color.blue;
    [SerializeField] Color m_HighlightedColor = Color.red;

    private Color m_InitialRendererComponentColor = Color.white;
    private Color m_InitialTextMeshProColor = Color.white;
    private bool m_Selected = false;

    public UnityEvent<SelectableGameObject> OnSelect = null;
    public UnityEvent<SelectableGameObject> OnDeselect = null;

    public bool Selected
    {
        get
        {
            return m_Selected;
        }
    }

    public void SelectGameObject()
    {
        if (!m_Selected)
        {
            m_Selected = true;
            if (m_SelectableRendererComponent != null)
            {
                m_SelectableRendererComponent.material.color = m_SelectedColor;
            }
            if (m_SelectableTextMeshPro != null)
            {
                m_SelectableTextMeshPro.color = m_SelectedColor;
            }
            OnSelect.Invoke(this);
        }
    }

    public void DeselectGameObject()
    {
        if (m_Selected)
        {
            m_Selected = false;
            if (m_SelectableRendererComponent != null)
            {
                m_SelectableRendererComponent.material.color = m_InitialRendererComponentColor;
            }
            if (m_SelectableTextMeshPro != null)
            {
                m_SelectableTextMeshPro.color = m_InitialRendererComponentColor;
            }
            OnDeselect.Invoke(this);
        }
    }

    private void Awake()
    {
        if (m_SelectableRendererComponent != null)
        {
            m_InitialRendererComponentColor = m_SelectableRendererComponent.material.color;
        }
        if (m_SelectableTextMeshPro != null)
        {
            m_InitialTextMeshProColor = m_SelectableTextMeshPro.color;
        }
    }

    private void OnMouseEnter()
    {
        if (m_SelectableRendererComponent != null)
        {
            m_SelectableRendererComponent.material.color = m_HighlightedColor;
        }
        if (m_SelectableTextMeshPro != null)
        {
            m_SelectableTextMeshPro.color = m_HighlightedColor;
        }
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        if (m_Selected)
        {
            DeselectGameObject();
        }
        else
        {
            SelectGameObject();
        }
    }

    private void OnMouseOver()
    {
        //TODO: could fade over time
        //m_SelectableRendererComponent.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }

    void OnMouseExit()
    {
        if (!m_Selected)
        {
            if (m_SelectableRendererComponent != null)
            {
                m_SelectableRendererComponent.material.color = m_InitialRendererComponentColor;
            }
            if (m_SelectableTextMeshPro != null)
            {
                m_SelectableTextMeshPro.color = m_InitialRendererComponentColor;
            }
        }
    }
}

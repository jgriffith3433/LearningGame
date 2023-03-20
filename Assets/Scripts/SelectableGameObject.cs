using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableGameObject : MonoBehaviour
{
    [SerializeField] Renderer m_SelectableRendererComponent = null;
    [SerializeField] Color m_SelectedColor = Color.blue;
    [SerializeField] Color m_HighlightedColor = Color.red;

    private Color m_InitialColor = Color.white;
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
            m_SelectableRendererComponent.material.color = m_SelectedColor;
            OnSelect.Invoke(this);
        }
    }

    public void DeselectGameObject()
    {
        if (m_Selected)
        {
            m_Selected = false;
            m_SelectableRendererComponent.material.color = m_InitialColor;
            OnDeselect.Invoke(this);
        }
    }

    private void Awake()
    {
        m_InitialColor = m_SelectableRendererComponent.material.color;
    }

    private void OnMouseEnter()
    {
        m_SelectableRendererComponent.material.color = m_HighlightedColor;
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
            m_SelectableRendererComponent.material.color = m_InitialColor;
        }
    }
}

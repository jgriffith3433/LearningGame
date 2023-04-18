using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class AnswerSelectionManager : Singleton<AnswerSelectionManager>
{
    public List<SelectableGameObject> m_AnswerGameObjects = null;

    private SelectableGameObject m_SelectedAnswer = null;

    public SelectableGameObject SelectedAnswer
    {
        get
        {
            return m_SelectedAnswer;
        }
    }

    public void OnAnswerSelected(SelectableGameObject answer)
    {
        foreach (var answerGameObject in m_AnswerGameObjects)
        {
            if (answerGameObject != answer)
            {
                answerGameObject.DeselectGameObject();
            }
        }
        m_SelectedAnswer = answer;
    }

    public void OnAnswerDeselected(GameObject go)
    {
        m_SelectedAnswer = null;
    }
}


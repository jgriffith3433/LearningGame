using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnLevelSelected(GameObject go) {
        GameManager.Instance.UIController.ChangeLevelSelectionText(go.name);
    }

    public void OnLevelDeselected(GameObject go)
    {
        GameManager.Instance.UIController.ChangeLevelSelectionText("Select Level");
    }
}


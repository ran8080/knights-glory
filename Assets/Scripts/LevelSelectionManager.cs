using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour 
{
    // Config params 
    [SerializeField] LevelButton[] levelButtons;

    // stat
    private int chosenLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Lock all levels that are bigger then level selection index + 1
        int levelAt = PlayerPrefsController.GetLevelAt(CrossSceneVars.levelsIndexOffset); // 2
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + CrossSceneVars.selectionLevelBuildIndex >= levelAt)
            {
                levelButtons[i].LockLevelButton();
            }
        }
    }

    public void SetChosenLevel(int newLevelIndex) {
        chosenLevelIndex = newLevelIndex;
    }

    public void LoadChosenLevel() {
        FindObjectOfType<LevelLoader>().LoadWithLoadingScreen(chosenLevelIndex);
    }

}

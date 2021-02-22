using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {

    [SerializeField] LevelButton[] levelButtons;

    // state
    private int chosenLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Lock all levels that are bigger then level selection index + 1
        int levelAt = PlayerPrefsController.GetLevelAt(CrossSceneVars.levelsIndexOffset); // 2
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Debug.Log("i = " + i);
            if (i + CrossSceneVars.selectionLevelBuildIndex >= levelAt)
            {
                Debug.Log(string.Format("i ({0}) + selectionLevelBI ({1}) > levelAt {2}",
                    i, CrossSceneVars.selectionLevelBuildIndex, levelAt));
                Debug.Log(string.Format("Locking buttion in i ({0}), for: world {1} level {1}.",
                    i, levelButtons[i].levelInfo.worldNumber, levelButtons[i].levelInfo.levelNumber));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton: MonoBehaviour
{
    [SerializeField] public Level levelInfo;
    [SerializeField] protected Text levelHeadlineText;
    [SerializeField] Sprite lockedLevelSprite;
    [SerializeField] Sprite unlockedLevelSprite;

    // Stats
    protected bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        levelHeadlineText.text = levelInfo.levelNumber.ToString();
    }

    void Update()
    {
        if (isLocked && levelHeadlineText.text != "") {
            levelHeadlineText.text = "";
        } else if (!isLocked && levelHeadlineText.text == "") {
            levelHeadlineText.text = levelInfo.levelNumber.ToString();
        }
    }

    public void ChooseSelectedLevel()
    {
        var print = string.Format("Button pressed for level {0}, loading scene in index {1}",
             levelInfo.levelNumber, levelInfo.GetLevelIndex());

        FindObjectOfType<LevelSelectionManager>().SetChosenLevel(levelInfo.GetLevelIndex());
        FindObjectOfType<LevelSelectionManager>().LoadChosenLevel();
        Debug.Log(print);
    }

    public void LockLevelButton()
    {
        isLocked = true;
        gameObject.GetComponent<Button>().interactable = false;
        var buttonImage = GetComponent<Image>();
        buttonImage.sprite = lockedLevelSprite;
    }

    public void UnlockLevelButton()
    {
        isLocked = false;
        gameObject.GetComponent<Button>().interactable = true;
        var buttonImage = GetComponent<Image>();
        buttonImage.sprite = unlockedLevelSprite;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    
    public Level level;
    public Text levelNumberText;

    // Start is called before the first frame update
    void Start()
    {
        levelNumberText.text = level.levelNumber.ToString();
    }
}

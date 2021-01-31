using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldLevelButton: LevelButton 
{
    // Start is called before the first frame update
    void Start()
    {
        levelHeadlineText.text = string.Format("World {0}", levelInfo.worldNumber);
    }

    private void Update() {}

}

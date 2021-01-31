using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public string worldName;
    public int worldNumber;
    public int levelNumber;
    
    // constants
    private static string LEVEL_SCENE_NAME_TEMPLATE = "World {0} - Level {1}";

    public int GetLevelIndex() {
        Debug.Log(string.Format("Computed Level index = {0}",
             worldNumber + levelNumber + CrossSceneVars.selectionLevelBuildIndex));
        return worldNumber + levelNumber + CrossSceneVars.selectionLevelBuildIndex;
    }

    public string GetLevelSceneName() {
        return string.Format(LEVEL_SCENE_NAME_TEMPLATE, worldNumber, levelNumber);
    }
}

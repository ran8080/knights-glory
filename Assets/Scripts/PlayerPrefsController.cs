using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_AT_KEY = "levelAt";

    // Values bounderies
    const float MIN_VOLUME = 0;
    const float MAX_VOLUME = 1;

    const int MIN_DIFFICULTY = 1;
    const int MAX_DIFFICULTY = 5;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            Debug.Log("Master volume set to " + volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        var defaultVolume = 0.4f;
        if (PlayerPrefs.HasKey(MASTER_VOLUME_KEY)) 
        { 
            defaultVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
        }
        else
        {
            OptionsController optionsController = FindObjectOfType < OptionsController >();
            if (optionsController) {
                defaultVolume = optionsController.defaultVolume;
            }
        }
        return defaultVolume;
    }

    public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            Debug.Log("Game difficulty set to " + difficulty);
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty is out of range");
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }

    public static int GetLevelAt(int currentLevelIndex)
    { 
        return PlayerPrefs.GetInt(LEVEL_AT_KEY, currentLevelIndex);
    }

    public static void SetLevelAt(int currentLevelIndex)
    { 
        PlayerPrefs.SetInt(LEVEL_AT_KEY, currentLevelIndex);
    }

    public static void DeleteLevelAt()
    {
        PlayerPrefs.DeleteKey(LEVEL_AT_KEY);
    }
}

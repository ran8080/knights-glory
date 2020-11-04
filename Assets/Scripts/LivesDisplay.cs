using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] int baseLives = 3;
    Text livesText;
    int lives = 1;

    private void Start()
    {
        InitializeLivesByDifficulty();
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void InitializeLivesByDifficulty()
    {
        lives = baseLives - PlayerPrefsController.GetDifficulty();
    }

    private void UpdateDisplay() 
    {
        livesText.text = lives.ToString();
    }

    private void Lose()
    {
        FindObjectOfType<LevelController>().HandleLoseCondition();
    }

    public bool HaveEnoughlives(int amount)
    {
        return lives >= amount;
    }

    public void DealDamage(int amount)
    {
        if (lives >= amount) 
        { 
            lives -= amount;
            UpdateDisplay();
        }
        else
        {
            Lose();
        }
    }
}

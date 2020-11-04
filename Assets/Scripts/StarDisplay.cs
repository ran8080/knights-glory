using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{

    [SerializeField] int stars = 800;
    [SerializeField] int autoStarsToGenerate = 5;
    [SerializeField] int autoStarsGeneratorTimeout = 3;
    Text starText;

    private void Start()
    {
        //InitialzeStarsByDifficulty();  // TODO add that later
        starText = GetComponent<Text>();
        UpdateDisplay();
        StartCoroutine(AutoGenerateStartsEverySeconds());
    }

    private IEnumerator AutoGenerateStartsEverySeconds()
    {
        while (true) { 
            yield return new WaitForSeconds(autoStarsGeneratorTimeout);
            AddStars(autoStarsToGenerate);
        }
    }

    private void InitialzeStarsByDifficulty()
    {
        stars /= PlayerPrefsController.GetDifficulty();
        autoStarsToGenerate /= PlayerPrefsController.GetDifficulty();
    }

    private void UpdateDisplay() 
    {
        starText.text = stars.ToString();
    }

    public bool HaveEnoughStars(int amount)
    {
        return stars >= amount;
    }

    public void AddStars(int amount)
    {
        stars += amount;
        UpdateDisplay();
    }

    public void SpandStars(int amount)
    {
        if (stars >= amount) 
        { 
            stars -= amount;
            UpdateDisplay();
        }
    }
}

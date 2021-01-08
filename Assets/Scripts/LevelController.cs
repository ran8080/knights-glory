using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLable;
    [SerializeField] GameObject loseLable;
    [SerializeField] GameObject LevelOptionsLable;
    [SerializeField] float waitToLoadNextLevel = 4f;
    [SerializeField] float waitToLoadDialog = 0.31f;

    [SerializeField] Slide[] menuSlides;
    [SerializeField] TMP_Text slideTextComponent;
    [SerializeField] GameObject slideImageGameObject;
    [SerializeField] GameObject slidesLabel;

    [SerializeField] AudioClip clickSFX;
    [SerializeField] [Range(0f, 1f)] float clickSFXVolume = 0.5f;

    [SerializeField] GameObject[] buttonSets;

    int currentButtonSetIndex = 0;
    int currentSlideIndex = 0;
    int currentLevelNumber = 0;
    string currentWorldName = "";

    int numberOfAttackers = 0;
    bool levelTimerFinished = false;
    bool didLoseLevel = false;

    void Start()
    {
        if (buttonSets.Length < 1) {
            Debug.LogError("No button sets defined for level. If level is not a menu level, game is not playable.");
        } else {
            buttonSets[0].SetActive(true);
        }

        winLable.SetActive(false);
        loseLable.SetActive(false);

        if (menuSlides.Length > 0 && CrossSceneVars.enableDialog) {
            CrossSceneVars.enableDialog = true;
            StartCoroutine(WaitAndStartDialog());
        }
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if (numberOfAttackers <= 0 && levelTimerFinished && !didLoseLevel)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }


    private void StopSpawners()
    {
        AttackerSpawner[] spanwers = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spanwers)
        {
            spawner.StopSpawning();
        }
    }

    public void HandleLoseCondition()
    {
        didLoseLevel = true;
        loseLable.SetActive(true);
        Time.timeScale = 0;  // Set game speed to 0 to prevent weird stuff from happaning
    }

    private IEnumerator HandleWinCondition()
    {
        winLable.SetActive(true);
        //Time.timeScale = 0;  // Set game speed to 0 to prevent weird stuff from happaning
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoadNextLevel);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleOptionsToolbarOpened()
    {
        LevelOptionsLable.SetActive(true);
        Time.timeScale = 0;  // Set game speed to 0 to prevent weird stuff from happaning
    }

    public void HandleOptionsToolbarClosed()
    {
        LevelOptionsLable.SetActive(false);
        Time.timeScale = 1;  // Set game speed back to 1 to continue playing 
    }

    private IEnumerator WaitAndStartDialog() {
        yield return new WaitForSeconds(waitToLoadDialog);
        Time.timeScale = 0; 

        // Start presenting dialog
        slidesLabel.SetActive(true);
        SetSlideCurrentContent();
    }

    private void SetSlideCurrentContent() { 
        slideTextComponent.text = menuSlides[currentSlideIndex].slideText;
        slideImageGameObject.GetComponent<Image>().sprite = menuSlides[currentSlideIndex].slideSprite;
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position, clickSFXVolume);
    }

    public void LoadNextSlide() {
        if (menuSlides.Length > currentSlideIndex + 1)
        {
            currentSlideIndex++;
            SetSlideCurrentContent();
        }
        else {
            SkipSlidesMenu();
        }
    }

    public void LoadPreviousSlide() { 
        if (currentSlideIndex > 0) {
            currentSlideIndex--;
            SetSlideCurrentContent();
        }
    }

    public void SkipSlidesMenu() {
        Time.timeScale = 1;
        slidesLabel.SetActive(false);
    }

    public void SwitchButtonSet() {
        buttonSets[currentButtonSetIndex].SetActive(false);

        if (buttonSets.Length > currentButtonSetIndex + 1) {
            currentButtonSetIndex++;
        } else {
            currentButtonSetIndex = 0;
        }
        buttonSets[currentButtonSetIndex].SetActive(true);
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position, clickSFXVolume);
    }
}

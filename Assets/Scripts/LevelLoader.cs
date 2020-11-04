using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float loadingScreenTimeInSeconds = 4f;
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime;
    [SerializeField] string currentWorldName = "";
    
    // States
    int currentSceneIndex;
    const string SPLASH_SCREEN_TAG = "splash screen";
    const string WORLD_INTRO_TAG = "world intro";

    public void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (gameObject.tag == SPLASH_SCREEN_TAG)
        {
            StartCoroutine(WaitSecondsAndLoadNext(loadingScreenTimeInSeconds));
        }
        else if (gameObject.tag == WORLD_INTRO_TAG)
        {
            // TODO set currentWorldText
            if (currentWorldName.Length > 0) {
                Debug.Log("set world name to " + currentWorldName);
            }
            StartCoroutine(WaitSecondsAndLoadNext(loadingScreenTimeInSeconds));
        }
    }
    
    IEnumerator WaitSecondsAndLoadNext(float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);
        LoadNextScene();
    }
    
    public void LoadYouWin() {
        SceneManager.LoadScene("You Win");
    }

    public void ReloadCurrentLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadOptionsScreen() {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadLoadingScene() { 
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene() {
        // Not checking scene index bounderies
        Debug.Log("Loading scene in index: " + currentSceneIndex + 1);
        //SceneManager.LoadScene(currentSceneIndex + 1);
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // TODO here set level number text to currentLevel
        transition.SetTrigger("Start"); 
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

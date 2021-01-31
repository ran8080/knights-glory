using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float loadingScreenTimeInSeconds = 4f;
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime; [SerializeField] string currentWorldName = ""; 
    // States
    [SerializeField] int currentSceneIndex;
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
        CrossSceneVars.enableDialog = false;
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(currentSceneIndex));
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadOptionsScreen() {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadLevelSelectionScreen() {
        SceneManager.LoadScene("Level Selection Screen");
    }

    public void LoadLoadingScene() { 
        SceneManager.LoadScene(0);
    }

    public void LoadLevelAtIndex(int levelIndex) {
        StartCoroutine(LoadLevel(levelIndex));
    }

    public void LoadNextScene() {
        // Not checking scene index bounderies
        var nextLevelIndex = currentSceneIndex + 1;
        Debug.Log("Loading scene in index: " + nextLevelIndex);
        CrossSceneVars.enableDialog = true;
        UnlockNextLevelInPrefs(nextLevelIndex);
        StartCoroutine(LoadLevel(nextLevelIndex));
    }

    public void DeleteLevelAtAndLoadNextScene() {
        PlayerPrefsController.DeleteLevelAt();
        LoadNextScene();
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

    private void UnlockNextLevelInPrefs(int nextLevelIndex) 
    {
        Debug.Log(string.Format("Current level scene {0}, next level scene {1}",
            currentSceneIndex, nextLevelIndex));
        // Update levelAt only if next level is bigger then current levelAt value 
        if (PlayerPrefsController.GetLevelAt(CrossSceneVars.selectionLevelBuildIndex) < nextLevelIndex) {
            Debug.Log(string.Format("PlayerPref.GetLevelAt = {0} < currentSceneIndex = {1}", CrossSceneVars.selectionLevelBuildIndex, currentSceneIndex));
            Debug.Log(string.Format("Setting Level At to {0}", nextLevelIndex));
            PlayerPrefsController.SetLevelAt(nextLevelIndex);
        }
    }
}

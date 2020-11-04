using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLable;
    [SerializeField] GameObject loseLable;
    [SerializeField] GameObject LevelOptionsLable;
    [SerializeField] float waitToLoad = 4f;

    int currentLevelNumber = 0;
    string currentWorldName = "";

    int numberOfAttackers = 0;
    bool levelTimerFinished = false;
    bool didLoseLevel = false;

    void Start()
    {
        winLable.SetActive(false);
        loseLable.SetActive(false);
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
        yield return new WaitForSeconds(waitToLoad);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level Timer In SECONDS")]
    [SerializeField] float levelTime = 10;

    // State variables
    bool triggeredTimerFinished = false;
    Slider levelTimeSlider;

    private void Start()
    {
        //InitizlizeLevelTimeByDifficulty();
        levelTimeSlider = GetComponent<Slider>();
    }

    private void InitizlizeLevelTimeByDifficulty()
    {
        levelTime *= PlayerPrefsController.GetDifficulty();
    }

    // Update is called once per frame
    private void Update()
    {
        if (triggeredTimerFinished) { return; }
        levelTimeSlider.value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        if (timerFinished) {
            FindObjectOfType<LevelController>().LevelTimerFinished();
            triggeredTimerFinished = true;
        }
    }
}

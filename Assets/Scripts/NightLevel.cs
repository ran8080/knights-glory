using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLevel : MonoBehaviour
{
    
    [SerializeField] float waitTime = 3f;
    [SerializeField] float shortWaitTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowNightTimeLable());
    }

    private IEnumerator ShowNightTimeLable()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}

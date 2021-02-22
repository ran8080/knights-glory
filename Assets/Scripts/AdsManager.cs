using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    
    [SerializeField] bool testMode = false;
    [SerializeField] bool adsEnabled = false;
    [SerializeField] int adsRandomisationFactor = 3;
    
    // Constants 
    string GooglePlay_ID = "";
    int ADS_SUCCESSFULL_RANDOM_GUESS = 1;

    void Start()
    {
        if (!string.IsNullOrEmpty(GooglePlay_ID))
        {
            Advertisement.Initialize(GooglePlay_ID, testMode);    
        }
    }

    public void DisplayAdWithRandomFactor() {
        var result = Random.Range(0, adsRandomisationFactor);
        if (result == ADS_SUCCESSFULL_RANDOM_GUESS && 
            IsLevelIndexValidForAdsDisplay() && 
            adsEnabled &&
            !string.IsNullOrEmpty(GooglePlay_ID))
        { 
            Advertisement.Show();
        }
        else 
        {
            Debug.Log("Not presenting ad this time, random result differs from " + 
                "random guess, or not advanced level enough");
        }
    }

    public void DisplayInterstitialAd() {
        Advertisement.Show();
    }

    private bool IsLevelIndexValidForAdsDisplay() {
        return SceneManager.GetActiveScene().buildIndex >= CrossSceneVars.adsDisplayingMinIndex;
    }

}

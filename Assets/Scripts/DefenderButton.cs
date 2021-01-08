using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{

    [SerializeField] Defender defenderPrefab;
    [SerializeField] AudioClip clickSFX;
    [SerializeField] [Range(0f, 1f)] float clickSFXVolume = 0.2f;
    public GameObject glow;

    private void Start()
    {
        LabelButtonWithCost();
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        if (costText)
        {
            costText.text = defenderPrefab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        DisableAllDefenderButtons();
        //gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position, clickSFXVolume);
        glow.SetActive(true);


        // Pass defender prefab to defender spawner
        FindObjectOfType<DefenderSpawner>().setSelectedDefender(defenderPrefab);
    }

    private static void DisableAllDefenderButtons()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton button in buttons)
        {
            //button.GetComponent<SpriteRenderer>().color = new Color32(72, 72, 72, 255);
            button.GetComponent<DefenderButton>().glow.gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{

    [SerializeField] Defender defenderPrefab;
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
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach(DefenderButton button in buttons)
        {
            //button.GetComponent<SpriteRenderer>().color = new Color32(72, 72, 72, 255);
            button.GetComponent<DefenderButton>().glow.gameObject.SetActive(false);
        }
        //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        glow.SetActive(true);
        
        
        // Pass defender prefab to defender spawner
        FindObjectOfType<DefenderSpawner>().setSelectedDefender(defenderPrefab);
    }
}

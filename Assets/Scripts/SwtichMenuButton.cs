using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwtichMenuButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController == null) {
            Debug.LogError("Couldn't find LevelController object. Serious Error.");
            return;
        } else { 
            Debug.Log("Switching button set.");
            levelController.SwitchButtonSet();
            DisableAllDefenderButtons();
        }
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

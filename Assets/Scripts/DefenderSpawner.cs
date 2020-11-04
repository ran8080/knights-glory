using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    // States
    [SerializeField] Defender defender;
    GameObject defenderParent;

    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME); // Find a game object by name
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void setSelectedDefender(Defender defenderToSelect) 
    {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos) 
    {
        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();
        if (starDisplay.HaveEnoughStars(defenderCost))
        { 
            SpawnDefender(gridPos);
            starDisplay.SpandStars(defenderCost);
        }
    }

    private bool isSquareOccupied(Vector2 gridPos)
    {
        Defender[] defendersOnScreen = FindObjectsOfType<Defender>();
        foreach (Defender defender in defendersOnScreen) { 
            var defenderGridPos = new Vector2(defender.transform.position.x,
                 defender.transform.position.y);

            if (defenderGridPos.Equals(gridPos)) 
            {
                Debug.Log("Square is Occupied...");
                return true; 
            }
        }

        Debug.Log("Square is free, allowing to spawn defender...");
        return false;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Convert the vector to Position on world view, then to Grid position
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newY;
        if (rawWorldPos.y > 0f) { 
            newY = Mathf.FloorToInt(rawWorldPos.y);
        } else { 
            newY = Mathf.RoundToInt(rawWorldPos.y);
        }

        float newX = Mathf.RoundToInt(rawWorldPos.x);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 roundedPos) 
    { 
        if (!defender) { return; }
        Defender newDefender = Instantiate(defender,
             roundedPos, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}

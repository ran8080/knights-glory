using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] GameObject newGamePopUpLabel;

    public void NewGamePopUpOpened()
    {
        newGamePopUpLabel.SetActive(true);
    }

    public void NewGamePopUpClosed()
    {
        newGamePopUpLabel.SetActive(false);
    }
}

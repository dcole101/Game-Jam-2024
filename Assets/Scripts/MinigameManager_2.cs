using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager_2 : MonoBehaviour
{
    public Canvas minigameCanvas;
    public MiniGameBase MiniGame;
    public GameObject sfxController;

    private void Start()
    {
        MiniGame = new DontDrinkPoison();
        MiniGame.SetupGame(minigameCanvas, 1.0f);
    }

    void Update()
    {
        MiniGame.UpdateGame(sfxController, Time.deltaTime);
    }

    void SwitchMiniGame(int gameID)
    {
        switch(gameID)
        {
            case 0:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager_2 : MonoBehaviour
{
    public Canvas minigameCanvas;
    public MiniGameBase MiniGame;

    private void Start()
    {
        MiniGame = new DontDrinkPoison();
        MiniGame.SetupGame(minigameCanvas);
    }

    void Update()
    {
        MiniGame.UpdateGame(Time.deltaTime);
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

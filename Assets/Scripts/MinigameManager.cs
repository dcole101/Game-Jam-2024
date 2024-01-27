using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public Canvas minigameCanvas;
    private MiniGameBase MiniGame;

    private void Start()
    {
        MiniGame = new WhackMole();
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

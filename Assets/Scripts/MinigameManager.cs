using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public Canvas minigameCanvas;
    public MiniGameBase MiniGame;

    bool gameRunning;
    bool gameWon;

    float timeElapsed = 5;
    float gameEndDelay = 2;

    private void Start()
    {
        gameRunning = true;
        gameWon = false;

        MiniGame = new HoopJump();
        MiniGame.SetupGame(minigameCanvas);
    }

    void Update()
    {
        if (gameRunning)
        {
            int gameState = MiniGame.UpdateGame(Time.deltaTime);

            if (gameState != 0)
            {
                if (gameState == 1)
                {
                    gameWon = true;
                }

                timeElapsed = 0;
                gameRunning = false;
            }
        }
        else if (timeElapsed < gameEndDelay) {
            timeElapsed += Time.deltaTime;
            
            if (gameWon)
            {
                //victory effect
            }
            else
            {
                //Lost Effect
            }

            if (timeElapsed >= gameEndDelay)
            {
                MiniGame.ResetGame();
            }
        }
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

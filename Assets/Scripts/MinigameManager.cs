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

    int minigameCount = 3;
    int currentGameID = -1;

    int health = 5;

    float speedModifier;

    private void Start()
    {
        speedModifier = 1;
        //gameRunning = true;
        gameWon = false;
        SwitchMiniGame();

        //MiniGame = new HoopJump();
        //MiniGame.SetupGame(minigameCanvas);


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
                else
                {
                    health--;
                }

                Debug.Log(health);

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
        else
        {
            speedModifier += 0.1f;
            SwitchMiniGame();
        }
    }

    void SwitchMiniGame()
    {
        gameRunning = true;
        int gameID = GetRandomGameID();

        if (MiniGame != null)
        {
            MiniGame.ResetGame();
        }

        switch (gameID)
        {
            case 0:
                MiniGame = new WhackMole();
                break;
            case 1:
                MiniGame = new RockPaperScissors();
                break;
            case 2:
                MiniGame = new HoopJump();
                break;
        }

        MiniGame.SetupGame(minigameCanvas, speedModifier);

    }

    int GetRandomGameID()
    {
        int newGameID = Random.Range(0, minigameCount);
        if (newGameID == currentGameID || newGameID == 1)
        {
            return GetRandomGameID();
        }
        currentGameID = newGameID;
        return newGameID;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public Canvas minigameCanvas;
    public MiniGameBase MiniGame;

    public Image jesterTimer;
    public float jesterSpeed = 5000f;
    public float lerpedX = 0;

    bool lerpStarted = false; 

    bool gameRunning;
    bool gameWon;
    bool isGameEndDelay;

    float timeElapsed = 5;
    float gameEndDelay = 2;

    float timerTime = 0f;

    int minigameCount = 4;

    int health = 5;

    float speedModifier;

    int currentID = -1;
    List<int> availableIDs;

    private void Start()
    {
        availableIDs = new List<int>();

        speedModifier = 1;
        for (int i = 0; i < minigameCount; i++)
        {
            availableIDs.Add(i);
        }

        gameWon = false;
        SwitchMiniGame();
        timerTime = MiniGame.timeLimit;
    }

    void Update()
    {

        if (gameRunning)
        {
            int gameState = MiniGame.UpdateGame(Time.deltaTime);

            Debug.Log(MiniGame.timeLimit/timerTime);

            // Calculate the lerp position based on the elapsed time

            lerpedX = Mathf.Lerp(-550, 550, MiniGame.timeLimit / timerTime);
           
      
            // Update the position of the UI Image
            jesterTimer.transform.position = new Vector2(lerpedX, 45);

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
                isGameEndDelay = true;
            }
        }
        else if (timeElapsed < gameEndDelay && isGameEndDelay) {
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
                isGameEndDelay = false;
                MiniGame.ResetGame();
                timeElapsed = 0;
            }
        }
        else if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
        else if (availableIDs.Count <= 0)
        {
            timeElapsed += Time.deltaTime;
            jesterTimer.gameObject.SetActive(false);
            //Level UP effects
            if (timeElapsed > 2)
            {
                LevelUp();
                jesterTimer.gameObject.SetActive(true);
            }
        }
        else
        {
            SwitchMiniGame();
            jesterTimer.transform.position = new Vector2(400,45);

            timerTime = MiniGame.timeLimit;
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
            case 3:
                MiniGame = new DontDrinkPoison();
                break;
        }

        MiniGame.SetupGame(minigameCanvas, speedModifier);

    }

    int GetRandomGameID()
    {

        int idPos = Random.Range(0, availableIDs.Count);
        int newGameID = availableIDs[idPos];

        if (newGameID == currentID)
        {
            return GetRandomGameID();
        }
        currentID = newGameID;
        availableIDs.RemoveAt(idPos);
        return newGameID;
    }

    void LevelUp()
    {
        for (int i = 0; i < minigameCount; i++)
        {
            availableIDs.Add(i);
        }

        Debug.Log("LEVEL UP");
        speedModifier += 0.2f;

    }
}

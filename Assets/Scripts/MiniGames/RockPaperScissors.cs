using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RockPaperScissors : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    //Rock, Paper, Scissors Icons that users clicks to select move.
    GameObject rockIcon;
    GameObject paperIcon;
    GameObject scissorsIcon;

    //Hands making the Rock, Paper, Scissors Hand Sign.
    GameObject playerRock;
    GameObject playerPaper;
    GameObject playerScissors;
    GameObject opponentRock;
    GameObject opponentPaper;
    GameObject opponentScissors;

    //What handsign the player/opponent is currently making.
    GameObject playerHand;
    GameObject opponentHand;

    GameObject uiParent;

    int minigameSuccess;
    float speedModif;

    public override void SetupGame(Canvas gameArea, float speedModifier)
    {
        //GameObject.Find("GameManager").GetComponent<MinigameManager>().jesterTimer.transform.position = new Vector2(400, 45);

        speedModif = speedModifier;
        minigameSuccess = 0;

        timeLimit = 5;
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        int randomSign;

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("RockPaperScissors").ToList();
        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "rockIcon")
            {
                rockIcon = gameUI;
            }
            else if (gameUI.name == "paperIcon")
            {
                paperIcon = gameUI;
            }
            else if (gameUI.name == "scissorsIcon")
            {
                scissorsIcon = gameUI;
            }
            else if (gameUI.name == "playerHand")
            {
                playerHand = gameUI;
            }
            else if(gameUI.name == "opponentHand")
            {
                opponentHand = gameUI;
            }
            else if (gameUI.name == "playerRock")
            {
                playerRock = gameUI;
            }
            else if (gameUI.name == "playerPaper")
            {
                playerPaper = gameUI;
            }
            else if (gameUI.name == "playerScissors")
            {
                playerScissors = gameUI;
            }
            else if (gameUI.name == "opponentRock")
            {
                opponentRock = gameUI;
            }
            else if (gameUI.name == "opponentPaper")
            {
                opponentPaper = gameUI;
            }
            else if (gameUI.name == "opponentScissors")
            {
                opponentScissors = gameUI;
            }
            else if( gameUI.name == "RockPaperScissors")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }

        randomSign = Random.Range(0, 3);
        if(randomSign == 0)
        {
            opponentHand.GetComponent<Image>().sprite = opponentRock.GetComponent<Image>().sprite;
        }
        else if(randomSign == 1)
        {
            opponentHand.GetComponent<Image>().sprite = opponentPaper.GetComponent<Image>().sprite;
        }
        else if(randomSign == 2)
        {
            opponentHand.GetComponent<Image>().sprite = opponentScissors.GetComponent<Image>().sprite;
        }
    }

    public override int UpdateGame(float deltaTime)
    {
        deltaTime *= speedModif;
        timeLimit -= deltaTime;

        if((timeLimit <= 0 || minigameSuccess == -1) && minigameSuccess != 1) 
        {
            Debug.Log("YOU LOST!");
            return -1;
        }

        if(minigameSuccess == 1) 
        {
            Debug.Log("YOU WON!");
            return 1;
        }

        raycastResult = gameController.UpdateControls(deltaTime);
        if (raycastResult != null)
        {
            foreach (RaycastResult result in raycastResult)
            {
                Debug.Log("Hit " + result.gameObject.name);
                if (result.gameObject.name == "Ground")
                {
                    break;
                }

                if (result.gameObject == rockIcon) //Player Selects Rock
                {
                    playerHand.GetComponent<Image>().sprite = playerRock.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite)
                    {
                        minigameSuccess = -1;
                    }
                    else if(opponentHand.GetComponent<Image>().color == opponentScissors.GetComponent<Image>().color) 
                    {
                        minigameSuccess = 1;
                    }
                }
                else if(result.gameObject == paperIcon) //Player Selects Paper
                {
                    playerHand.GetComponent<Image>().sprite = playerPaper.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentScissors.GetComponent<Image>().sprite)
                    {
                        minigameSuccess = -1;
                    }
                    else if (opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite)
                    {
                        minigameSuccess = 1;
                    }
                }
                else if(result.gameObject == scissorsIcon) //Player Selects Scissors
                {
                    playerHand.GetComponent<Image>().sprite = playerScissors.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentScissors.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite)
                    {
                        minigameSuccess = -1;
                    }
                    else if (opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite)
                    {
                        minigameSuccess = 1;
                    }
                }
            }
        }
        return 0;
    }

    public override void ResetGame()
    {
        playerHand.GetComponent<Image>().sprite = null;
        opponentHand.GetComponent<Image>().sprite = null;
        uiParent.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RockPaperScissors : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    float rotateSpeed;
    float xSpeed;

    float elapsedTime;
    
    bool iconClicked;

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
        iconClicked = false;
        elapsedTime = 0;
        rotateSpeed = 200f;
        xSpeed = 1600f;

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
            else if (gameUI.name == "opponentHand")
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
            else if (gameUI.name == "RockPaperScissors")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }

        playerHand.transform.position = new Vector3(-750 + 540, -475 + 960, 0);
        opponentHand.transform.position = new Vector3(750 + 540, -475 + 960, 0);
        playerHand.transform.rotation = Quaternion.Euler(0, 0, 45);
        opponentHand.transform.rotation = Quaternion.Euler(0, 0, 315);
        playerHand.SetActive(false);

        randomSign = Random.Range(0, 3);
        if (randomSign == 0)
        {
            opponentHand.GetComponent<Image>().sprite = opponentRock.GetComponent<Image>().sprite;
        }
        else if (randomSign == 1)
        {
            opponentHand.GetComponent<Image>().sprite = opponentPaper.GetComponent<Image>().sprite;
        }
        else if (randomSign == 2)
        {
            opponentHand.GetComponent<Image>().sprite = opponentScissors.GetComponent<Image>().sprite;
        }
    }

    public override int UpdateGame(GameObject sfxController, float deltaTime)
    {
        deltaTime *= speedModif;
        timeLimit -= deltaTime;
        elapsedTime += Time.deltaTime;

        if ((timeLimit <= 0 || minigameSuccess == -1) && minigameSuccess != 1)
        {
            Debug.Log("YOU LOST!");
            if (elapsedTime > 0.5f)
            {
                return -1;
            }
        }

        if (minigameSuccess == 1)
        {
            Debug.Log("YOU WON!");
            if (elapsedTime > 0.5f)
            {
                return 1;
            }
        }

        if (opponentHand.transform.rotation.eulerAngles.z >= 45)
        {
            opponentHand.GetComponent<RectTransform>().Rotate(Vector3.forward * rotateSpeed * deltaTime);
        }

        if(opponentHand.GetComponent<RectTransform>().position.x >= 850)
        {
            opponentHand.GetComponent<RectTransform>().position = new Vector2(opponentHand.transform.position.x + (-xSpeed * deltaTime), opponentHand.transform.position.y);
        }

        if(iconClicked == true)
        {
            if (playerHand.GetComponent<RectTransform>().position.x <= 250)
            {
                playerHand.GetComponent<RectTransform>().position = new Vector2(playerHand.transform.position.x + (xSpeed * deltaTime), playerHand.transform.position.y);
            }

            if (playerHand.transform.rotation.z > 0)
            {
                Debug.Log(playerHand.transform.rotation.z);
                playerHand.GetComponent<RectTransform>().Rotate(Vector3.forward * -rotateSpeed * deltaTime);
            }
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

                    playerHand.SetActive(true);
                    iconClicked = true;

                    playerHand.GetComponent<Image>().sprite = playerRock.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite)
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = -1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                        }
                    }
                    else if(opponentHand.GetComponent<Image>().sprite == opponentScissors.GetComponent<Image>().sprite) 
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = 1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[9]);

                        }
                    }
                }
                else if(result.gameObject == paperIcon) //Player Selects Paper
                {

                    playerHand.SetActive(true);
                    iconClicked = true;

                    playerHand.GetComponent<Image>().sprite = playerPaper.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentScissors.GetComponent<Image>().sprite)
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = -1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                        }
                    }
                    else if (opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite)
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = 1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[9]);

                        }
                    }
                }
                else if(result.gameObject == scissorsIcon) //Player Selects Scissors
                {

                    playerHand.SetActive(true);
                    iconClicked = true;

                    playerHand.GetComponent<Image>().sprite = playerScissors.GetComponent<Image>().sprite;
                    if (opponentHand.GetComponent<Image>().sprite == opponentScissors.GetComponent<Image>().sprite || opponentHand.GetComponent<Image>().sprite == opponentRock.GetComponent<Image>().sprite)
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = -1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                        }
                    }
                    else if (opponentHand.GetComponent<Image>().sprite == opponentPaper.GetComponent<Image>().sprite)
                    {
                        if (iconClicked == true)
                        {
                            elapsedTime = 0;
                            minigameSuccess = 1;
                            sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[9]);

                        }
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

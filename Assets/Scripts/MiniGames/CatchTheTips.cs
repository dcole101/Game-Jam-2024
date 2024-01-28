using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatchTheTips : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    //Rock, Paper, Scissors Icons that users clicks to select move.

    GameObject hat;
    GameObject coin1;
    GameObject coin2;
    GameObject coin3;

    float ySpeed = 3500;
    GameObject uiParent;
    float elapsedTime;

    bool droppedAllCoins;


    int minigameSuccess;
    float speedModif;

    public override void SetupGame(Canvas gameArea, float speedModifier)
    {
        speedModif = speedModifier;
        minigameSuccess = 0;

        timeLimit = 5;
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("CatchTheTips").ToList();
        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "Hat")
            {
                hat = gameUI;
            }
            else if (gameUI.name == "Coin 1")
            {
                coin1 = gameUI;
            }
            else if (gameUI.name == "Coin 2")
            {
                coin2 = gameUI;
            }
            else if (gameUI.name == "Coin 3")
            {
                coin3 = gameUI;
            }
            else if (gameUI.name == "CatchTheTips")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }
    }

    public override int UpdateGame(float deltaTime)
    {
        deltaTime *= speedModif;
        elapsedTime += Time.deltaTime;
        timeLimit -= deltaTime;

        if ((timeLimit <= 0 || minigameSuccess == -1) && minigameSuccess != 1)
        {
            //Debug.Log("YOU LOST!");
            return -1;
        }

        if (minigameSuccess == 1)
        {
            //Debug.Log("YOU WON!");
            return 1;
        }

        hat.transform.position = Input.mousePosition;

        if(droppedAllCoins == false)
        {

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
            }
        }
        return 0;
    }

    public override void ResetGame()
    {

        uiParent.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }
}

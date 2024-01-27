using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DontDrinkPoison : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    //Rock, Paper, Scissors Icons that users clicks to select move.
    
    GameObject cup1;
    GameObject cup2;
    GameObject cup3;
    GameObject poisonIcon1;
    GameObject poisonIcon2;

    bool hasShuffled;

    Transform[] waypoints = new Transform[7];
    float xSpeed = 3000f;

    GameObject uiParent;

    int minigameSuccess;

    public override void SetupGame(Canvas gameArea)
    {
        minigameSuccess = 0;

        timeLimit = 5;
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        hasShuffled = false;

        int randomSign;

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("DontDrinkPoison").ToList();
        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "Poison Icon 1")
            {
                poisonIcon1 = gameUI;
            }
            else if (gameUI.name == "Poison Icon 2")
            {
                poisonIcon2 = gameUI;
            }
            if (gameUI.name == "Cup 1")
            {
                cup1 = gameUI;
            }
            else if (gameUI.name == "Cup 2")
            {
                cup2 = gameUI;
            }
            else if (gameUI.name == "Cup 3")
            {
                cup3 = gameUI;
                Debug.Log(cup3);
            }
            else if (gameUI.name == "Waypoint 1")
            {
                waypoints[0] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 2")
            {
                waypoints[1] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 3")
            {
                waypoints[2] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 4")
            {
                waypoints[3] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 5")
            {
                waypoints[4] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 6")
            {
                waypoints[5] = gameUI.transform;
            }
            else if (gameUI.name == "Waypoint 7")
            {
                waypoints[6] = gameUI.transform;
            }
            else if (gameUI.name == "DontDrinkPoison")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }

        randomSign = Random.Range(0, 3);
        if (randomSign == 0)
        {
            
        }
        else if (randomSign == 1)
        {

        }
        else if (randomSign == 2)
        {

        }
    }

    public override int UpdateGame(float deltaTime)
    {
        timeLimit -= deltaTime;

        if ((timeLimit <= 0 || minigameSuccess == -1) && minigameSuccess != 1)
        {
            Debug.Log("YOU LOST!");
            return -1;
        }

        if (minigameSuccess == 1)
        {
            Debug.Log("YOU WON!");
            return 1;
        }

        if (hasShuffled == false)
        {
            //Step 1
            if(cup3.GetComponent<Transform>().position.y < 400) 
            {
                cup3.GetComponent<Transform>().position = new Vector2(cup3.transform.position.x + (xSpeed * deltaTime), cup3.transform.position.y);
            }


            //hasShuffled = true;
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

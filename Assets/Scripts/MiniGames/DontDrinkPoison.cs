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
    GameObject poisonIcon3;

    bool cup1Poisoned;
    bool cup2Poisoned;
    bool cup3Poisoned;

    bool cup1Safe;
    bool cup2Safe;
    bool cup3Safe;

    bool cupsPoisoned;
    bool hasShuffled;
    bool step1Complete;
    bool step2Complete;
    bool step3Complete;

    float xSpeed = 3500;

    GameObject uiParent;

    float elapsedTime;


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

        cup1Poisoned = false;
        cup2Poisoned = false;
        cup3Poisoned = false;


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
            else if (gameUI.name == "Poison Icon 3")
            {
                poisonIcon3 = gameUI;
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
            }

            else if (gameUI.name == "DontDrinkPoison")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }

        cup1.GetComponent<Transform>().position = new Vector3(190, 260, 0);
        cup2.GetComponent<Transform>().position = new Vector3(540, 260, 0);
        cup3.GetComponent<Transform>().position = new Vector3(890, 260, 0);

        randomSign = Random.Range(0, 3);
        if (randomSign == 0)
        {

            cup1Poisoned = true;
            cup2Poisoned = true;

            cup3Safe = true;

            poisonIcon1.GetComponent<Image>().color = new Color(poisonIcon1.GetComponent<Image>().color.r, poisonIcon1.GetComponent<Image>().color.g, poisonIcon1.GetComponent<Image>().color.b, 1);
            poisonIcon2.GetComponent<Image>().color = new Color(poisonIcon2.GetComponent<Image>().color.r, poisonIcon2.GetComponent<Image>().color.g, poisonIcon2.GetComponent<Image>().color.b, 1);
        }
        else if (randomSign == 1)
        {
            cup1Poisoned = true;
            cup3Poisoned = true;

            cup2Safe = true;

            poisonIcon1.GetComponent<Image>().color = new Color(poisonIcon1.GetComponent<Image>().color.r, poisonIcon1.GetComponent<Image>().color.g, poisonIcon1.GetComponent<Image>().color.b, 1);
            poisonIcon3.GetComponent<Image>().color = new Color(poisonIcon3.GetComponent<Image>().color.r, poisonIcon3.GetComponent<Image>().color.g, poisonIcon3.GetComponent<Image>().color.b, 1);
        }
        else if (randomSign == 2)
        {
            cup2Poisoned = true;
            cup3Poisoned = true;

            cup1Safe = true;

            poisonIcon2.GetComponent<Image>().color = new Color(poisonIcon2.GetComponent<Image>().color.r, poisonIcon2.GetComponent<Image>().color.g, poisonIcon2.GetComponent<Image>().color.b, 1);
            poisonIcon3.GetComponent<Image>().color = new Color(poisonIcon3.GetComponent<Image>().color.r, poisonIcon3.GetComponent<Image>().color.g, poisonIcon3.GetComponent<Image>().color.b, 1);
        }

        cupsPoisoned = false;

        hasShuffled = false;
        step1Complete = false;
        step2Complete = true;
        step3Complete = true;

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

        if (cupsPoisoned == false && elapsedTime >= 0.5f)
        {
            if (cup1Poisoned == true && poisonIcon1.GetComponent<Image>().color.a >= 0)
            {
                poisonIcon1.GetComponent<Image>().color = new Color(poisonIcon1.GetComponent<Image>().color.r, poisonIcon1.GetComponent<Image>().color.g, poisonIcon1.GetComponent<Image>().color.b, (poisonIcon1.GetComponent<Image>().color.a + ((-500 * deltaTime) / 255)));
            }
            else if (cup1Poisoned == true && poisonIcon1.GetComponent<Image>().color.a < 0)
            {
                cupsPoisoned = true;
                hasShuffled = false;
                elapsedTime = 0;
            }

            if (cup2Poisoned == true && poisonIcon2.GetComponent<Image>().color.a >= 0)
            {
                poisonIcon2.GetComponent<Image>().color = new Color(poisonIcon2.GetComponent<Image>().color.r, poisonIcon2.GetComponent<Image>().color.g, poisonIcon2.GetComponent<Image>().color.b, (poisonIcon2.GetComponent<Image>().color.a + ((-500 * deltaTime) / 255)));
            }
            else if (cup2Poisoned == true && poisonIcon2.GetComponent<Image>().color.a < 0)
            {
                cupsPoisoned = true;
                hasShuffled = false;
                elapsedTime = 0;
            }

            if (cup3Poisoned == true && poisonIcon3.GetComponent<Image>().color.a >= 0)
            {
                poisonIcon3.GetComponent<Image>().color = new Color(poisonIcon3.GetComponent<Image>().color.r, poisonIcon3.GetComponent<Image>().color.g, poisonIcon3.GetComponent<Image>().color.b, (poisonIcon3.GetComponent<Image>().color.a + ((-500 * deltaTime) / 255)));
            }
            else if (cup3Poisoned == true && poisonIcon3.GetComponent<Image>().color.a < 0)
            {
                cupsPoisoned = true;
                hasShuffled = false;
                elapsedTime = 0;
            }
        }


        if (hasShuffled == false && cupsPoisoned == true)
        {
            //Step 1
            if (step1Complete == false)
            {
                if (cup3.GetComponent<RectTransform>().position.x > 540)
                {
                    cup3.GetComponent<RectTransform>().position = new Vector2(cup3.transform.position.x + (-xSpeed * deltaTime), cup3.transform.position.y);
                    cup2.GetComponent<RectTransform>().position = new Vector2(cup2.transform.position.x + (xSpeed * deltaTime), cup1.transform.position.y);
                }
                else
                {
                    cup3.GetComponent<RectTransform>().position = new Vector2(540, cup1.transform.position.y);
                    cup2.GetComponent<RectTransform>().position = new Vector2(940, cup1.transform.position.y);

                    step1Complete = true;
                    step2Complete = false;
                    elapsedTime = 0;
                }
            }

            if (step2Complete == false && elapsedTime > 0.25f)
            {
                //Step 2
                if (cup3.GetComponent<RectTransform>().position.x > 140)
                {
                    cup3.GetComponent<RectTransform>().position = new Vector2(cup3.transform.position.x + (-xSpeed * deltaTime), cup3.transform.position.y);
                    cup1.GetComponent<RectTransform>().position = new Vector2(cup1.transform.position.x + (xSpeed * deltaTime), cup1.transform.position.y);
                }
                else
                {
                    cup3.GetComponent<RectTransform>().position = new Vector2(140, cup1.transform.position.y);
                    cup1.GetComponent<RectTransform>().position = new Vector2(540, cup1.transform.position.y);

                    step2Complete = true;
                    step3Complete = false;
                    elapsedTime = 0;
                }
            }


            if (step3Complete == false && elapsedTime > 0.25f)
            {
                //Step 3
                if (cup2.GetComponent<RectTransform>().position.x > 540)
                {
                    cup2.GetComponent<RectTransform>().position = new Vector2(cup2.transform.position.x + (-xSpeed * deltaTime), cup2.transform.position.y);
                    cup1.GetComponent<RectTransform>().position = new Vector2(cup1.transform.position.x + (xSpeed * deltaTime), cup1.transform.position.y);
                }
                else
                {
                    cup2.GetComponent<RectTransform>().position = new Vector2(540, cup2.transform.position.y);
                    cup1.GetComponent<RectTransform>().position = new Vector2(940, cup1.transform.position.y);

                    step3Complete = true;
                    elapsedTime = 0;
                    hasShuffled = true;
                }
            }
        }



        raycastResult = gameController.UpdateControls(deltaTime);
        if (raycastResult != null && hasShuffled == true)
        {
            foreach (RaycastResult result in raycastResult)
            {
                Debug.Log("Hit " + result.gameObject.name);
                if (result.gameObject.name == "Ground")
                {
                    break;
                }
                else if (result.gameObject.name == "Cup 1")
                {
                    if (cup1Safe == true)
                    {
                        minigameSuccess = 1;
                        Debug.Log("YOU DID IT!");
                    }
                    else if (cup1Poisoned == true)
                    {
                        minigameSuccess = -1;
                        Debug.Log("WRONG");
                    }
                }
                else if (result.gameObject.name == "Cup 2")
                {
                    if (cup2Safe == true)
                    {
                        minigameSuccess = 1;
                        Debug.Log("YOU DID IT!");
                    }
                    else if (cup2Poisoned == true)
                    {
                        minigameSuccess = -1;
                        Debug.Log("WRONG");
                    }
                }
                else if (result.gameObject.name == "Cup 3")
                {
                    if (cup3Safe == true)
                    {
                        minigameSuccess = 1;
                        Debug.Log("YOU DID IT!");
                    }
                    else if (cup3Poisoned == true)
                    {
                        minigameSuccess = -1;
                        Debug.Log("WRONG");
                    }
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

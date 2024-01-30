using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    float ySpeed = 650f;
    GameObject uiParent;
    float elapsedTime;

    bool droppedAllCoins;
    bool droppedCoin1;
    bool droppedCoin2;
    bool droppedCoin3;

    bool coin1Collected;
    bool coin2Collected;
    bool coin3Collected;

    int minigameSuccess;
    float speedModif;

    public override void SetupGame(Canvas gameArea, float speedModifier)
    {

        speedModif = speedModifier;
        minigameSuccess = 0;

        timeLimit = 4;
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
                uiParent.GetComponent<Transform>().position = new Vector2(270, 480);
            }
        }
        int randomOrder = Random.Range(0, 6);

        //230,540,850
        if(randomOrder == 0)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(230, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(540, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(850, 955);
        }
        else if(randomOrder == 1)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(230, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(850, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(540, 955);
        }
        else if(randomOrder == 2)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(540, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(230, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(850, 955);
        }
        else if (randomOrder == 3)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(540, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(850, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(230, 955);
        }
        else if (randomOrder == 4)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(850, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(230, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(540, 955);
        }
        else if (randomOrder == 5)
        {
            coin1.GetComponent<RectTransform>().position = new Vector2(850, 955);
            coin2.GetComponent<RectTransform>().position = new Vector2(540, 955);
            coin3.GetComponent<RectTransform>().position = new Vector2(230, 955);
        }

        coin1.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        coin2.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        coin3.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);


        elapsedTime = 0;
        droppedAllCoins = false;
        droppedCoin1 = false;
        droppedCoin2 = false;
        droppedCoin3 = false;

        coin1Collected = false;
        coin2Collected = false;
        coin3Collected = false;
    }

    public override int UpdateGame(GameObject sfxController, float deltaTime)
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

        if (Input.mousePosition.y < 900)
        {
            hat.transform.position = Input.mousePosition;
        }

        if(droppedAllCoins == false)
        {
            if (droppedCoin1 == false)
            {
                if (coin1.transform.position.y >= 0)
                {
                    coin1.GetComponent<RectTransform>().position = new Vector2(coin1.transform.position.x,coin1.transform.position.y +(-ySpeed * deltaTime));
                }
                else if(coin1.transform.position.y <= 0 && coin1Collected == false)
                {
                    droppedCoin1 = true;
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                    return -1;
                }
            }
            if(droppedCoin2 == false && elapsedTime > 0.5f)
            {
                if (coin2.transform.position.y >= 0)
                {
                    coin2.GetComponent<RectTransform>().position = new Vector2(coin2.transform.position.x, coin2.transform.position.y + (-ySpeed * deltaTime));
                }
                else if(coin2.transform.position.y <= 0 && coin2Collected == false)
                {
                    droppedCoin2 = true;
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                    return -1;
                }
            }
            
            if(droppedCoin3 == false && elapsedTime > 1f)
            {
                if (coin3.transform.position.y >= 0)
                {
                    coin3.GetComponent<RectTransform>().position = new Vector2(coin3.transform.position.x, coin3.transform.position.y + (-ySpeed * deltaTime));
                }
                else if(coin3.transform.position.y <= 0 && coin3Collected == false)
                {
                    droppedCoin3 = true;
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[8]);

                    return -1;
                }
            }

            if((coin1.transform.position.x >= hat.transform.position.x - 70 && coin1.transform.position.x <= hat.transform.position.x + 70) && (coin1.transform.position.y >= hat.transform.position.y - 70 && coin1.transform.position.y <= hat.transform.position.y + 70))
            {
                if(coin1Collected == false) 
                {
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[Random.Range(0, 3)]);
                }
                coin1Collected = true;
                coin1.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);

            }

            if ((coin2.transform.position.x >= hat.transform.position.x - 70 && coin2.transform.position.x <= hat.transform.position.x + 70) && (coin2.transform.position.y >= hat.transform.position.y - 70 && coin2.transform.position.y <= hat.transform.position.y + 70))
            {
                if (coin2Collected == false)
                {
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[Random.Range(0, 3)]);
                }
                coin2Collected = true;
                coin2.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
            }

            if ((coin3.transform.position.x >= hat.transform.position.x - 70 && coin3.transform.position.x <= hat.transform.position.x + 70) && (coin3.transform.position.y >= hat.transform.position.y - 70 && coin3.transform.position.y <= hat.transform.position.y + 70))
            {
                if (coin3Collected == false)
                {
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[Random.Range(0, 3)]);
                }
                coin3Collected = true;
                coin3.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
            }

            if (droppedCoin1 == true && droppedCoin2 == true && droppedCoin3 == true)
            {
                droppedAllCoins = true;
            }
        }

        if(coin1Collected == true && coin2Collected == true && coin3Collected == true)
        {
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
            }
        }
        return 0;
    }

    public override void ResetGame()
    {

        uiParent.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }
}

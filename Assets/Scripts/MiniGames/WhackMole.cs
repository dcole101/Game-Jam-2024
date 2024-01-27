using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WhackMole : MiniGameBase
{
    float timeElapsed;
    List<RaycastResult> raycastResult = null;

    GameObject[] moles = { null, null, null};

    GameObject uiParent;

    bool[] moleUp = { false, false, false};
    bool[] moleActive = { false, false, false };
    bool[] moleHit = { false, false, false };

    float origHeight;
    float moleHeight;
    float moleSpeed;

    int hitGoal = 1;

    float m_speedModifier;
    public override void SetupGame(Canvas gameArea, float speedModifier)
    {
        m_speedModifier = speedModifier;
        timeLimit = 4;
        timeElapsed= 0;
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        moleSpeed = 500;

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("WhackMole").ToList();

        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "Mole1")
            {
                moles[0] = gameUI;
            }
            else if (gameUI.name == "Mole2")
            {
                moles[1] = gameUI;
            }
            else if (gameUI.name == "Mole3")
            {
                moles[2] = gameUI;
            }
            else if (gameUI.name == "WhackMole")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540,960);
            }
        }

        origHeight = 265;
        moleHeight = moles[0].GetComponent<RectTransform>().rect.height * moles[0].GetComponent<Transform>().localScale.y;

        for (int i = 0; i < 3; i++)
        {
            moles[i].GetComponent<Transform>().position = new Vector2(moles[i].GetComponent<Transform>().position.x, origHeight);

            moleUp[i] = true;
            moleActive[i] = false;
            moleHit[i] = false;
        }
    }

    public override int UpdateGame(float deltaTime)
    {
        deltaTime *= m_speedModifier;
        timeElapsed += deltaTime;
        timeLimit -= deltaTime;

        if (timeLimit < 0)
        {
            //Debug.Log("TIME OVER");
            return -1;
        }

        raycastResult = gameController.UpdateControls(deltaTime);
        if (raycastResult != null)
        {
            foreach (RaycastResult result in raycastResult)
            {
                //Debug.Log("Hit " + result.gameObject.name);
                if (result.gameObject.name == "Ground")
                {
                    break;
                }

                if (result.gameObject == moles[0])
                {
                    moleActive[0] = false;
                    moleHit[0] = true;
                }

                if (result.gameObject == moles[1])
                {
                    moleActive[1] = false;
                    moleHit[1] = true;
                }

                if (result.gameObject == moles[2])
                {
                    moleActive[2] = false;
                    moleHit[2] = true;
                }
            }
        }

        int hitCount = 0;
        foreach (bool hit in moleHit)
        {
            if (hit)
            {
                hitCount++;
            }
        }

        if (hitCount >= hitGoal)
        {
            //Debug.Log("GOAL");
            return 1;
        }

        if (timeElapsed >= 1)
        {
            timeElapsed -= 1;
            moleActive[getRandomMole()] = true;
        } 

        UpdateMoles(deltaTime);
        return 0;
    }

    public override void ResetGame()
    {
        for (int i = 0; i < 3; i++)
        {

            moleUp[i] = false;
            moleActive[i] = false;
            moleHit[i] = false;
        }

        uiParent.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }

    private void UpdateMoles(float deltaTime)
    {
        for (int i = 0; i < 3 ;i++)
        {
            if (moleActive[i])
            {
                if (moleUp[i])
                {
                    Vector3 mole1Pos = moles[i].GetComponent<Transform>().position;
                    moles[i].GetComponent<Transform>().position = new Vector2(mole1Pos.x, mole1Pos.y + (moleSpeed * deltaTime));

                    if (moles[i].GetComponent<Transform>().position.y - origHeight >= moleHeight)
                    {
                        moleUp[i] = false;
                    }
                }
                else
                {
                    Vector3 mole1Pos = moles[i].GetComponent<Transform>().position;
                    moles[i].GetComponent<Transform>().position = new Vector2(mole1Pos.x, mole1Pos.y - (moleSpeed * deltaTime));

                    if (moles[i].GetComponent<Transform>().position.y - origHeight <= 0)
                    {
                        moleUp[i] = true;
                        moleActive[i] = false;
                    }
                }
            }
        }
    }

    private int getRandomMole()
    {
        int randMole = Random.Range(0, 3);

        if (moleActive[randMole] || moleHit[randMole])
        {
            return getRandomMole();
        }
        return randMole;
    }
}

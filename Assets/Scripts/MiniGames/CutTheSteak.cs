using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutTheSteak : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    float m_speedModifier;
    GameObject steakUncut;
    GameObject steakCut;

    GameObject parentUi;

    bool m_isSteakCut;

    float steakOrigWidth;
    int steakCuts;
    public override void SetupGame(Canvas gameArea, float speedModifier)
    {
        m_speedModifier = speedModifier;

        timeLimit = 4;

        steakCuts = 10;
        steakOrigWidth = 420;

        m_isSteakCut = false;

        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("CutTheSteak").ToList();

        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "SteakUncut")
            {
                steakUncut = gameUI;
            }
            else if (gameUI.name == "SteakCut")
            {
                steakCut = gameUI;
            }
            else if (gameUI.name == "CutTheSteak")
            {
                parentUi = gameUI;
                parentUi.GetComponent<Transform>().position = new Vector2(540, 960);
            }
        }
        steakUncut.GetComponent<RectTransform>().sizeDelta = new Vector2(steakOrigWidth, steakUncut.GetComponent<RectTransform>().rect.height);
        steakUncut.GetComponent<Transform>().position = new Vector2(32 + 540, -572 + 960);
    }

    public override int UpdateGame(float deltaTime)
    {
        deltaTime *= m_speedModifier;
        timeLimit -= deltaTime;

        if (timeLimit <= 0)
        {
            Debug.Log("Fail");
            return -1;
        }
        if (m_isSteakCut)
        {
            Debug.Log("Success");
            return 1;
        }

        raycastResult = gameController.UpdateControls(deltaTime);
        if (raycastResult != null)
        {
            foreach (RaycastResult result in raycastResult)
            {
                if (result.gameObject.name == "SteakUncut")
                {
                    CutSteak();
                }
            }
        }

        return 0;

    }

    public override void ResetGame()
    {
        parentUi.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }

    private void CutSteak()
    {
        steakCuts--;

        steakUncut.GetComponent<RectTransform>().sizeDelta -= new Vector2 (steakOrigWidth / 10, 0);
        steakUncut.GetComponent<Transform>().position += new Vector3(steakOrigWidth / 20, 0, 0);

        if (steakCuts <= 0)
        {
            m_isSteakCut = true;
        }
    }

}

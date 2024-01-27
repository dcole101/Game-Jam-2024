using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoopJump : MiniGameBase
{
    List<RaycastResult> raycastResult = null;

    GameObject uiParent;
    GameObject jester;
    GameObject hoopFront;
    GameObject hoopBack;

    float jesterDefaultHeight;
    float jesterSpeed;
    float jesterMaxSpeed;
    float gravity;

    bool isJumping;
    bool hoopJumpThrough;
    bool tempHoopJumpThrough;

    bool hoopShot;
    float hoopAngle;
    float hoopInitalSpeed;
    Vector2 initalHoopPos;
    Vector2 HoopInitialVelo;

    float elapsedTime;

    public override void SetupGame(Canvas gameArea)
    {
        timeLimit = 4;

        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);

        jesterDefaultHeight = 222;
        jesterSpeed = 0;
        jesterMaxSpeed = 1200;
        gravity = 1500;

        hoopShot = false;

        hoopAngle = Random.Range(15.0f, 35.0f);
        hoopInitalSpeed = 700;

        HoopInitialVelo.x = Mathf.Cos(hoopAngle * Mathf.Deg2Rad) * hoopInitalSpeed * -1;
        HoopInitialVelo.y = Mathf.Sin(hoopAngle * Mathf.Deg2Rad) * hoopInitalSpeed;

        List<GameObject> miniGameUI = GameObject.FindGameObjectsWithTag("HoopJump").ToList();

        foreach (GameObject gameUI in miniGameUI)
        {
            if (gameUI.name == "HoopJump")
            {
                uiParent = gameUI;
                uiParent.GetComponent<Transform>().position = new Vector2(540, 960);
            }
            else if (gameUI.name == "Jester")
            {
                jester = gameUI;
                
                Vector3 JesterPos = jester.GetComponent<Transform>().position;
                JesterPos.x += Random.Range(-150, 150);
                jester.GetComponent<Transform>().position = JesterPos;
            }
            else if (gameUI.name == "Hoop1")
            {
                hoopFront = gameUI;  
            }
            else if (gameUI.name == "Hoop2")
            {
                hoopBack = gameUI;
            }
        }

        initalHoopPos = hoopBack.GetComponent<Transform>().position;
    }

    public override int UpdateGame(float deltaTime)
    {
        timeLimit -= deltaTime;
        elapsedTime += deltaTime;

        if (hoopJumpThrough)
        {
            //Debug.Log("WIN");
            return 1;
        }
        else if (timeLimit <= 0)
        {
            //Debug.Log("Lost");
            return -1;
        }


        raycastResult = gameController.UpdateControls(deltaTime);
        if (raycastResult != null)
        {
            foreach (RaycastResult result in raycastResult)
            {
                if (result.gameObject.name == "BG")
                {
                    if (!isJumping)
                    {
                        isJumping = true;
                        jesterSpeed = jesterMaxSpeed;
                    }
                }
            }
        }

        if (isJumping)
        {
            UpdateJesterPos(deltaTime);

            CheckJesterHoop();
        }

        if (!hoopShot && elapsedTime >= 0.5)
        {
            hoopShot = true;
            elapsedTime = 0;
        }

        if (hoopShot)
        {
            UpdateHoopPos(deltaTime);
        }

        return 0;
    }

    public override void ResetGame()
    {
        hoopShot = false;
        hoopFront.GetComponent<Transform>().position = initalHoopPos;
        hoopBack.GetComponent<Transform>().position = initalHoopPos;

        jester.GetComponent<Transform>().position = new Vector2(jester.GetComponent<Transform>().position.x, jesterDefaultHeight);

        uiParent.GetComponent<Transform>().position = new Vector2(5000, 5000);
    }

    private void UpdateJesterPos(float deltaTime)
    {
        Vector3 jesterPosition = jester.GetComponent<Transform>().position;

        jesterPosition.y += jesterSpeed * deltaTime;
        jesterSpeed = Mathf.Max(Mathf.Min(jesterSpeed - (gravity * deltaTime), jesterMaxSpeed), -jesterMaxSpeed);

        jester.GetComponent<Transform>().position = jesterPosition;

        if (jester.GetComponent<Transform>().position.y <= jesterDefaultHeight)
        {
            jesterPosition.y = jesterDefaultHeight;
            jester.GetComponent<Transform>().position = jesterPosition;

            if (tempHoopJumpThrough)
            {
                hoopJumpThrough = true;
            }
            isJumping = false;
        }
    }

    private void UpdateHoopPos(float deltaTime)
    {
        Vector3 HoopPosition = hoopBack.GetComponent<Transform>().position;

        HoopPosition.x = (HoopInitialVelo.x * elapsedTime) + initalHoopPos.x;
        HoopPosition.y = (float)((HoopInitialVelo.y * elapsedTime) - (0.5 * (gravity / 4) * Mathf.Pow(2, elapsedTime))) + initalHoopPos.y;

        hoopBack.GetComponent<Transform>().position = HoopPosition;

        HoopPosition.y -= 15;
        hoopFront.GetComponent<Transform>().position = HoopPosition;
    }

    private void CheckJesterHoop()
    {
        Vector2 jesterPos = jester.GetComponent<Transform>().position;
        Vector2 hoopPos = hoopBack.GetComponent<Transform>().position;
        float hoopWidth = hoopBack.GetComponent<RectTransform>().rect.width;

        if (jesterPos.y >= hoopPos.y - 0.5 && jesterPos.y <= hoopPos.y + 0.5)
        {
            if (jesterPos.x <= hoopPos.x + (hoopWidth / 2) && jesterPos.x >= hoopPos.x - (hoopWidth / 2))
            {
                tempHoopJumpThrough = true;
            }
        }
    }
}

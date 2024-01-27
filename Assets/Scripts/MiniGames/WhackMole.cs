using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhackMole : MiniGameBase
{
    List<RaycastResult> raycastResult = null;
    public override void SetupGame(Canvas gameArea)
    {
        timeLimit = 5;
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);
    }

    public override void UpdateGame(float deltaTime)
    {
        

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
    }

    public override void ResetGame()
    {
        
    }
}

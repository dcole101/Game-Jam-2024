using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class game1 : MiniGameBase
{
    List<RaycastResult> results = null;
    public override void SetupGame(Canvas gameArea)
    {
        gameController = new PreciseClick();
        gameController.SetupControls(gameArea);
    }
    public override void UpdateGame(float deltaTime)
    {

        results =  gameController.UpdateControls(deltaTime);
        if (results != null)
        {
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
    }

    public override void ResetGame()
    {
        
    }
}

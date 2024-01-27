using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class game1 : MiniGameBase
{
    public Image test;
    public override void SetupGame()
    {
        //test = GameObject.Find("TestRaycast").GetComponent<Image>();
        //test.RegisterCallback<ClickEvent>(clickTest);
        gameController = new PreciseClick();
    }
    public override void UpdateGame()
    {

        gameController.UpdateControls();
        //Debug.Log("Game1");
    }

    private void clickTest(ClickEvent evt)
    {
        Debug.Log("AAAAAAAAAAAAAA");
    }
}

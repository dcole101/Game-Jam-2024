using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGameBase
{
    public BaseController gameController;
    public Canvas minigameCanvas;
    public float timeLimit;
    //public GameObject sfxController = GameObject.Find("SFXController");

    public abstract void SetupGame(Canvas gameArea, float speedModifier);
    public abstract int UpdateGame(GameObject sfxController, float deltaTime);
    public abstract void ResetGame();
}

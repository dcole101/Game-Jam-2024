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

    public abstract void SetupGame(Canvas gameArea);
    public abstract int UpdateGame(float deltaTime);
    public abstract void ResetGame();
}

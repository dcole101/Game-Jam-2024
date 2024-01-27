using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGameBase
{
    public BaseController gameController;
    public Canvas minigameCanvas;

    public abstract void SetupGame(Canvas gameArea);
    public abstract void UpdateGame(float deltaTime);
    public abstract void ResetGame();
}

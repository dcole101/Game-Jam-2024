using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameBase
{
    public BaseController gameController;

    public abstract void SetupGame();
    public abstract void UpdateGame();
}

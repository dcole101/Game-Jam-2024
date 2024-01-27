using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseController
{
    public abstract void SetupControls(Canvas gameArea);
    public abstract List<RaycastResult> UpdateControls(float deltaTime);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PreciseClick : BaseController
{
    // Update is called once per frame
    public override void UpdateControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.y <= Screen.height / 2)
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
        }   
    }
}

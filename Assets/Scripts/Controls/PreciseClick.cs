using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PreciseClick : BaseController
{
    Canvas minigameCanvas;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public override void SetupControls(Canvas gameArea)
    {
        minigameCanvas = gameArea;

        m_Raycaster = minigameCanvas.GetComponent<GraphicRaycaster>();
        m_EventSystem = minigameCanvas.GetComponent<EventSystem>();
    }

    // Update is called once per frame
    public override List<RaycastResult> UpdateControls(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.y <= Screen.height / 2)
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();

                m_Raycaster.Raycast(m_PointerEventData, results);

                return results;

            }
        }
        return null;
    }
}

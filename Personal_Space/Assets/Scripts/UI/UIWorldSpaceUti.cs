using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIUtil
{
    public class UIWorldSpaceUti
    {
        public Vector2 GetWorldPos(RectTransform CanvasRect, GameObject WorldObject, bool main)
        {
            //then you calculate the position of the UI element
            //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5f to get the correct position.
            Vector2 ViewportPosition = new Vector2(-200,-200);

            if (main)
            {
                if (Camera.main != null)
                {
                    ViewportPosition = Camera.main.WorldToViewportPoint(WorldObject.transform.position);
                }
            }
            else
            {
                if (Camera.current != null)
                {
                    ViewportPosition = Camera.current.WorldToViewportPoint(WorldObject.transform.position);
                }
            }
            Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            //UI_Element.anchoredPosition = WorldObject_ScreenPosition;
            return WorldObject_ScreenPosition;
        }

    }
}

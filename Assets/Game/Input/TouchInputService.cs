using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchInputService : ITouchInputService
{
    public bool TryGetTouch(out Touch touch)
    {
        touch = default;

        if (Input.touchCount == 0)
            return false;

        touch = Input.GetTouch(0);

        if (IsTouchOverUI(touch))
            return false;

        return true;
    }

    private bool IsTouchOverUI(Touch touch)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touch.position
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count > 0;
    }
}


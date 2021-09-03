using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }
    public Vector2 GetDirectionToClic(Vector2 headPos)
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos = _cam.ScreenToViewportPoint(mousePos);
        mousePos.y = 1;
        mousePos = _cam.ViewportToWorldPoint(mousePos);

        Vector2 directeon = new Vector2(mousePos.x - headPos.x, mousePos.y - headPos.y);

        return directeon;
    }
}

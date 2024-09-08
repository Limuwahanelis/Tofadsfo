using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass 
{
    public static Vector3 MousePos=>_mousePos;
    private static Vector2 _mousePos;
    public static void SetMousePos(Vector2 pos)
    {
        _mousePos = pos;
    }
}

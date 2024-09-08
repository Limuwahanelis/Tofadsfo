using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] RaycastFromCamera _raycastCam;
    public void OnMousePos(InputValue inputValue)
    {
        _raycastCam.SetMousePos(inputValue.Get<Vector2>());
    }

    public void OnClick()
    {
        _raycastCam.TryPress();
    }
}

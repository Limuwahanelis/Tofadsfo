using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] RaycastFromCamera _raycastCam;
    [SerializeField] PlayerMouseInteractions _mouseInteractions;
    [SerializeField] PauseSetter _pauseSetter;
    public void OnMousePos(InputValue inputValue)
    {
        HelperClass.SetMousePos(inputValue.Get<Vector2>());
    }

    public void OnClick()
    {
        _mouseInteractions.TryPress();
    }
    public void OnPause()
    {
        _pauseSetter.SetPause(!PauseSettings.IsGamePaused);
    }
}

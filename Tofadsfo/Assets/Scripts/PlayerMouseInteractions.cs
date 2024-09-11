using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInteractions : MonoBehaviour
{
    [SerializeField] RaycastFromCamera _cameraRaycast;
    [SerializeField] GameObject _productsMenu;
    private IInteractable _interactable;
    private bool _closeProductsMenu=true;
    private Vector3 _productsMenuPos;
    private bool _canInteract=true;

    public void SetInteraction(bool value)
    {
        _canInteract= value;
    }
    public void TryPress()
    {
        if (!_canInteract) return;
        Vector3 point;
        _interactable=_cameraRaycast.Raycast(out point,out float width);
        _productsMenuPos = point;
        if (_interactable==null)
        {
            if(_closeProductsMenu)
            {
                _productsMenu.SetActive(false);
            }
        }
        else
        {
            _interactable.Interact();
            _productsMenuPos.x -= width;
            _productsMenu.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_productsMenuPos);
        }
    }

    public void SetShouldCloseProductsMenu(bool value)
    {
        _closeProductsMenu = value;
    }
}

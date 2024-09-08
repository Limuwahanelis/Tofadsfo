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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryPress()
    {
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
            _productsMenuPos.x -= width;
            _productsMenu.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_productsMenuPos);
            _productsMenu.SetActive(true);
        }
    }

    public void SetShouldCloseProductsMenu(bool value)
    {
        _closeProductsMenu = value;
    }
}

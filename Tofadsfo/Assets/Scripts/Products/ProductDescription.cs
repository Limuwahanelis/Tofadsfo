using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductDescription : MonoBehaviour
{
    public ProductSO Product => _productSO;
    [SerializeField] Image _image;
    [SerializeField] ProductSO _productSO;

    private void OnValidate()
    {
        if (_productSO != null)
        {
            _image.sprite = _productSO.Icon;
        }
    }

}

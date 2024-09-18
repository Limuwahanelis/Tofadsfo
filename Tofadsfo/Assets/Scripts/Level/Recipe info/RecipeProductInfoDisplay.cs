using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeProductInfoDisplay : MonoBehaviour
{
    [SerializeField] Image _productIcon;
    [SerializeField] TMP_Text _productNumtext;

    public void SetUp(ProductSO product,int numberOfProduct)
    {
        _productIcon.sprite = product.Icon;
        _productNumtext.text=numberOfProduct.ToString();
    }
}

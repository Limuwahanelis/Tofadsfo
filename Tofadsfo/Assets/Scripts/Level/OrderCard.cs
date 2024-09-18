using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
    [SerializeField] RecipeSO _recipe;
    [SerializeField] Image _orderImage;
    [SerializeField] int _numberOfOrders;
    [SerializeField] TMP_Text _orderNameText;
    [SerializeField] TMP_Text _numberOfOrderstext;
    [SerializeField] TMP_Text _priceText;

    private void Start()
    {
        _orderImage.sprite = _recipe.Icon;
        _orderNameText.text = _recipe.name;
        _priceText.text =$"<color=green>{_recipe.Price}$";
        _numberOfOrderstext.text=_numberOfOrders.ToString();
    }
    public void SetRecipe(RecipeSO recipe,int numberOfOrders)
    {
        _recipe = recipe;
        _numberOfOrders = numberOfOrders;
    }

}

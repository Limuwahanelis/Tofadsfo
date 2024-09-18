using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfoDisplay : MonoBehaviour
{
    [SerializeField] GameObject _productDisplayPrefab;
    [SerializeField] Transform _productDisplayHolder;
    [SerializeField] Image _recipeImage;
    public void SetUp(Sprite image, RecipeSO.CraftingRecipeShort recipe)
    {
        _recipeImage.sprite = image;
        for (int i=0;i<recipe.productTypes.Length;i++) 
        {
            Instantiate(_productDisplayPrefab, _productDisplayHolder).GetComponent<RecipeProductInfoDisplay>().SetUp(recipe.productTypes[i], recipe.resourcesNum[i]);
        }
    }

}

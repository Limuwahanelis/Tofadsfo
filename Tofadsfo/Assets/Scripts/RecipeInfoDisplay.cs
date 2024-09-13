using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeInfoDisplay : MonoBehaviour
{
    [SerializeField] GameObject _productDisplayPrefab;
    [SerializeField] Transform _productDisplayHolder;
    public void SetUp(RecipeSO.CraftingRecipeShort recipe)
    {
        for(int i=0;i<recipe.productTypes.Length;i++) 
        {
            Instantiate(_productDisplayPrefab, _productDisplayHolder).GetComponent<RecipeProductInfoDisplay>().SetUp(recipe.productTypes[i], recipe.resourcesNum[i]);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelRecipesInfoDisplay : MonoBehaviour
{
    [SerializeField] LevelInfoSO _levelinfo;
    [SerializeField] GameObject _recipeInfoDisplayPrefab;
    [SerializeField] GameObject _recipeInfoHolder;

    private void Start()
    {
        for(int i = 0; i < _levelinfo.Orders.Count; i++) 
        {
            RecipeSO.CraftingRecipeShort recipe= ConvertRecipetoShortFormat(_levelinfo.Orders[i]);
            RecipeInfoDisplay recipeDisplay= Instantiate(_recipeInfoDisplayPrefab,_recipeInfoHolder.transform).GetComponent<RecipeInfoDisplay>();
            recipeDisplay.SetUp(_levelinfo.Orders[i].Icon, recipe);
        }
    }
    RecipeSO.CraftingRecipeShort ConvertRecipetoShortFormat(RecipeSO recipe)
    {
        RecipeSO.CraftingRecipeShort shortRecipe=new RecipeSO.CraftingRecipeShort();
        int numberOfDistinctResources = recipe.Ingredients.Distinct().Count();
        ProductSO[] types = new ProductSO[numberOfDistinctResources];
        int[] num = new int[numberOfDistinctResources];
        int j = 0;
        for (int i = 0; i < recipe.Ingredients.Count(); i++)
        {
            if (types.Contains(recipe.Ingredients[i])) continue;
            types[j] = recipe.Ingredients[i];
            j++;
        }
        for (int i = 0; i < recipe.Ingredients.Count(); i++)
        {
            int index = Array.IndexOf(types, recipe.Ingredients[i]);
            num[index]++;
        }
        shortRecipe.resourcesNum = num;
        shortRecipe.productTypes = types;
        return shortRecipe;
    }
}

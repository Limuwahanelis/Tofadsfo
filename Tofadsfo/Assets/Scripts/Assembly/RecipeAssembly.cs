using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeAssembly : MonoBehaviour
{
    public RecipeSO.CraftingRecipeShort Shortrecipe => _recipeShort;
    public RecipeSO Recipe => _recipe;
    [SerializeField] RecipeSO _recipe;
    [SerializeField] SpriteRenderer _spriteRenderer;
    private RecipeSO.CraftingRecipeShort _recipeShort;
    private RecipeSO.CraftingRecipeShort _collectedProductsShort;
    void Start()
    {
        ConvertRecipetoShortFormat();
       
    }
    public ProductSO Assemble()
    {
        if (GetMissingProduct() == null)
        {
            for (int i = 0; i < _recipeShort.productTypes.Length; i++)
            {
                _collectedProductsShort.resourcesNum[i] -= _recipeShort.resourcesNum[i];
            }
            return _recipe.Result;
        }
        return null;
    }
    public void AddIngredient(ProductSO ingredient)
    {
        int index = _collectedProductsShort.productTypes.ToList().IndexOf(ingredient);
        _collectedProductsShort.resourcesNum[index]++;
    }
    public ProductSO GetMissingProduct()
    {
        for(int i=0;i<_recipeShort.productTypes.Length;i++)
        {
            if (_collectedProductsShort.resourcesNum[i] < _recipeShort.resourcesNum[i]) return _recipeShort.productTypes[i];
        }
        return null;
    }
    private void ConvertRecipetoShortFormat()
    {
        int numberOfDistinctResources = _recipe.Ingredients.Distinct().Count();
        ProductSO[] types = new ProductSO[numberOfDistinctResources];
        int[] num = new int[numberOfDistinctResources];
        int j = 0;
        for (int i = 0; i < _recipe.Ingredients.Count(); i++)
        {
            if (types.Contains(_recipe.Ingredients[i])) continue;
            types[j] = _recipe.Ingredients[i];
            j++;
        }
        for (int i = 0; i < _recipe.Ingredients.Count(); i++)
        {
            int index = Array.IndexOf(types, _recipe.Ingredients[i]);
            num[index]++;
        }
        _recipeShort.resourcesNum = num;
        _recipeShort.productTypes = types;

        _collectedProductsShort.resourcesNum = new int[numberOfDistinctResources];
        _collectedProductsShort.productTypes= types;
    }
    private void OnValidate()
    {
        if(_recipe!=null)
        {
            _spriteRenderer.sprite=_recipe.Icon;
        }
    }
}

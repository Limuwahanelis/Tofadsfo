using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "Recipe")]
public class RecipeSO : ScriptableObject
{
    public struct CraftingRecipeShort
    {
        public ProductSO[] productTypes;
        public int[] resourcesNum;
    }
    public int Price => _price;
    public CraftingRecipeShort ShortForm
    {
        get
        {
            if (_shortForm.productTypes == null)
            {
                _shortForm = ConvertRecipetoShortFormat();
                return _shortForm;
            }
            else return _shortForm;
        }
    }
    public ProductSO Result => _result;
    public List<ProductSO> Ingredients => _ingredients;
    public Sprite Icon => _result.Icon;

    private CraftingRecipeShort _shortForm;
    [SerializeField] ProductSO _result;
    [SerializeField] List<ProductSO> _ingredients;
    [SerializeField] int _price;

    CraftingRecipeShort ConvertRecipetoShortFormat()
    {
        RecipeSO.CraftingRecipeShort shortRecipe = new RecipeSO.CraftingRecipeShort();
        int numberOfDistinctResources = Ingredients.Distinct().Count();
        ProductSO[] types = new ProductSO[numberOfDistinctResources];
        int[] num = new int[numberOfDistinctResources];
        int j = 0;
        for (int i = 0; i < Ingredients.Count(); i++)
        {
            if (types.Contains(Ingredients[i])) continue;
            types[j] = Ingredients[i];
            j++;
        }
        for (int i = 0; i < Ingredients.Count(); i++)
        {
            int index = Array.IndexOf(types, Ingredients[i]);
            num[index]++;
        }
        shortRecipe.resourcesNum = num;
        shortRecipe.productTypes = types;
        return shortRecipe;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelInfoSO))]
public class LevelInfoSOEditor: Editor
{
    SerializedProperty _money;
    SerializedProperty _orders;
    SerializedProperty _amountOfOrders;
    SerializedProperty _availableProducts;
    SerializedProperty _startingAmountOfIngredients;
    SerializedProperty _maximumAmountOfIngredients;
    SerializedProperty _levelTime;
    int _ordersNum = 0;
    int _ordersAmountNum;
    int _productsNum;
    int _StartinIngredientsNum;
    int _maximumIngredientsNum;
    private void OnEnable()
    {
        _levelTime = serializedObject.FindProperty("_levelTimeInSeconds");
        _orders = serializedObject.FindProperty("_orders");
        _amountOfOrders = serializedObject.FindProperty("_amountOfOrders");
        _money = serializedObject.FindProperty("_startingMoney");
        _availableProducts = serializedObject.FindProperty("_availableProducts");
        _startingAmountOfIngredients = serializedObject.FindProperty("_startingAmountOfIngredients");
        _maximumAmountOfIngredients = serializedObject.FindProperty("_maximumAmountOfIngredients");
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_levelTime);
        EditorGUILayout.PropertyField(_money);
        #region Orders
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_orders);
        if(EditorGUI.EndChangeCheck()) 
        {
            _ordersNum = _orders.arraySize;
            _ordersAmountNum = _amountOfOrders.arraySize;
            if(_ordersAmountNum<_ordersNum)
            {
                int diff = _ordersNum - _ordersAmountNum;
                for (int i = 0; i < diff; i++)
                {
                    _amountOfOrders.InsertArrayElementAtIndex(_ordersAmountNum + i);
                    _amountOfOrders.GetArrayElementAtIndex(_ordersAmountNum + i).intValue = 0;
                }
            }
            else
            {
                int diff = _ordersAmountNum-_ordersNum;
                for (int i = _ordersAmountNum-1; i>= _ordersAmountNum- diff; i--)
                {
                    _amountOfOrders.DeleteArrayElementAtIndex(i);
                }
            }
           
        }
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_amountOfOrders);
        if (EditorGUI.EndChangeCheck())
        {
            _ordersNum = _orders.arraySize;
            _ordersAmountNum = _amountOfOrders.arraySize;
            if (_ordersAmountNum < _ordersNum)
            {
                int diff = _ordersNum - _ordersAmountNum;
                for (int i = 0; i < diff; i++)
                {
                    _amountOfOrders.InsertArrayElementAtIndex(_ordersAmountNum + i);
                    _amountOfOrders.GetArrayElementAtIndex(_ordersAmountNum + i).intValue = 0;
                }
            }
        }
        #endregion
        #region Ingredients
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_availableProducts);
        if (EditorGUI.EndChangeCheck())
        {
            _productsNum = _availableProducts.arraySize;
            _StartinIngredientsNum = _startingAmountOfIngredients.arraySize;
            _maximumIngredientsNum = _maximumAmountOfIngredients.arraySize;
            if(_maximumIngredientsNum < _productsNum)
            {
                int diff = _productsNum - _maximumIngredientsNum;
                for (int i = 0; i < diff; i++)
                {
                    _maximumAmountOfIngredients.InsertArrayElementAtIndex(_maximumIngredientsNum + i);
                    _maximumAmountOfIngredients.GetArrayElementAtIndex(_maximumIngredientsNum + i).intValue = 0;
                }
            }
            else
            {
                int diff = _maximumIngredientsNum - _productsNum;
                for (int i = _maximumIngredientsNum - 1; i >= _maximumIngredientsNum - diff; i--)
                {
                    _maximumAmountOfIngredients.DeleteArrayElementAtIndex(i);
                }
            }
            if (_StartinIngredientsNum < _productsNum)
            {
                int diff = _productsNum - _StartinIngredientsNum;
                for (int i = 0; i < diff; i++)
                {
                    _startingAmountOfIngredients.InsertArrayElementAtIndex(_StartinIngredientsNum + i);
                    _startingAmountOfIngredients.GetArrayElementAtIndex(_StartinIngredientsNum + i).intValue = 0;
                }
            }
            else
            {
                int diff = _StartinIngredientsNum - _productsNum;
                for (int i = _StartinIngredientsNum - 1; i >= _StartinIngredientsNum - diff; i--)
                {
                    _startingAmountOfIngredients.DeleteArrayElementAtIndex(i);
                }
            }

        }
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_startingAmountOfIngredients);
        if (EditorGUI.EndChangeCheck())
        {
            _productsNum = _availableProducts.arraySize;
            _StartinIngredientsNum = _startingAmountOfIngredients.arraySize;
            if (_StartinIngredientsNum < _productsNum)
            {
                int diff = _productsNum - _StartinIngredientsNum;
                for (int i = 0; i < diff; i++)
                {
                    _startingAmountOfIngredients.InsertArrayElementAtIndex(_StartinIngredientsNum + i);
                    _startingAmountOfIngredients.GetArrayElementAtIndex(_StartinIngredientsNum + i).intValue = 0;
                }
            }
        }
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(_maximumAmountOfIngredients);
        if (EditorGUI.EndChangeCheck())
        {
            _productsNum = _availableProducts.arraySize;
            _maximumIngredientsNum = _maximumAmountOfIngredients.arraySize;
            if (_maximumIngredientsNum < _productsNum)
            {
                int diff = _productsNum - _maximumIngredientsNum;
                for (int i = 0; i < diff; i++)
                {
                    _maximumAmountOfIngredients.InsertArrayElementAtIndex(_maximumIngredientsNum + i);
                    _maximumAmountOfIngredients.GetArrayElementAtIndex(_maximumIngredientsNum + i).intValue = 0;
                }
            }
        }
        #endregion
        serializedObject.ApplyModifiedProperties();

    }
}
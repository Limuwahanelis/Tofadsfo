using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ProductShopCardInputTextCheck : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputTextField;
    public void CheckTextInt(string text)
    {
        int value;
        string toOutput;
        if (string.IsNullOrEmpty(text)) return;
        else
        {
            if (text[0]=='0')
            {
                if (text.Length > 1) 
                {
                    text =$"{text[1]}";
                }
            }
            value = int.Parse(text);
            value = math.clamp(value, 0, 999);
            toOutput = value.ToString();
            _inputTextField.text = toOutput;
        }
        

    }
}

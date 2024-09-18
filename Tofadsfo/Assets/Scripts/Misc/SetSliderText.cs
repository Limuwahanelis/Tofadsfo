using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetSliderText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public void SetText(float value)
    {

        _text.text = string.Format("{0:0.00}", value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, Header("DEBUG")] bool _debug;
#endif
    [Space]
    [SerializeField] LevelInfoSO _levelInfo;
    [SerializeField] LevelInfoDisplay _levelInfoDisplay;
    [SerializeField] PlayerMouseInteractions _mouseInteractions;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    [SerializeField] ClientSpawner _clientSpawner;
    [SerializeField] LevelTimeDisplay _levelTimeDisplay;
    private void Start()
    {
        _mouseInteractions.SetInteraction(false);
        _levelInfoDisplay.Show();
#if UNITY_EDITOR
        if (_debug)
        {
            _mouseInteractions.SetInteraction(true);
            _levelInfoDisplay.Hide();
        }
#endif
        _levelTimeDisplay.SetTime(_levelInfo.LevelTimeInSecnods);
        _tableProductsManagment.SetUp(_levelInfo.AvailableProducts);
        _levelInfoDisplay.SetOrders(_levelInfo);
       
    }

    public void SetUpSpawner()
    {
        _clientSpawner.SetUp(_levelInfo);
    }
}

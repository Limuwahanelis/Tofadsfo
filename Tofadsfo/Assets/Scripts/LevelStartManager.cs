using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartManager : MonoBehaviour
{
    [SerializeField] LevelInfoSO _levelInfo;
    [SerializeField] LevelInfoDisplay _levelInfoDisplay;
    [SerializeField] PlayerMouseInteractions _mouseInteractions;
    [SerializeField] TableProductsManagment _tableProductsManagment;
    [SerializeField] ClientSpawner _clientSpawner;
    private void Start()
    {
        _mouseInteractions.SetInteraction(false);
        _tableProductsManagment.SetUp(_levelInfo.AvailableProducts);
        _levelInfoDisplay.SetOrders(_levelInfo);
        _levelInfoDisplay.Show();
    }

    public void SetUpSpawner()
    {
        _clientSpawner.SetUp(_levelInfo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] ClientQueue _queue;
    [SerializeField] GameObject _clientPrefab;
    [SerializeField] Transform _clientSpawnTran;

    public void SpawnClient()
    {
        ClientController client= Instantiate(_clientPrefab, _clientSpawnTran.position,_clientPrefab.transform.rotation).GetComponent<ClientController>();
        client.AssignQueue(_queue);
    }
}

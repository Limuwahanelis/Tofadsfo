using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientQueue : MonoBehaviour
{
    public Action OnPlaceFreed;
    [SerializeField] List<ClientQueuePlace> _queuePlaces = new List<ClientQueuePlace>();

    public void SetQueuePlace(ClientQueuePlace place,bool istaken)
    {
        place.SetIsTaken(istaken);
    }
    public ClientQueuePlace GetFreePlace()
    {
        ClientQueuePlace place =_queuePlaces.First(x => x.Istaken == false);
        place.SetIsTaken(true);
        return place;
    }
}

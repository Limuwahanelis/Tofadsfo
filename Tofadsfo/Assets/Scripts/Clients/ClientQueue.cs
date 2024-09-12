using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientQueue : MonoBehaviour
{
    public Action<ClientQueuePlace> OnPlaceFreed;
    [SerializeField] List<ClientQueuePlace> _queuePlaces = new List<ClientQueuePlace>();

    public void SetQueuePlace(ClientQueuePlace place,bool istaken)
    {
        place.SetIsTaken(istaken);
        if(!istaken) OnPlaceFreed?.Invoke(place);
    }
    public int GetPlaceIndex(ClientQueuePlace place)
    {
        return _queuePlaces.IndexOf(place);
    }
    public bool HasFreePlace()
    {
        return _queuePlaces.Exists(x => x.Istaken == false);
    }
    public ClientQueuePlace GetFreePlace()
    {
        ClientQueuePlace place =_queuePlaces.First(x => x.Istaken == false);
        place.SetIsTaken(true);
        return place;
    }
}

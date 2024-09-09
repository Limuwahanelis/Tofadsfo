using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTest : MonoBehaviour
{
    [SerializeField] TableWithProducts _table1;
    [SerializeField] TableWithProducts _table2;
    [SerializeField] AStarPathfinding _pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        _pathfinding.DivideMapIntoTiles();
        _pathfinding.SetTargetTiles(Vector3ToInt(_table1.transform.position), Vector3ToInt(_table2.transform.position));
        _pathfinding.StartLooking();
    }
    public void DD(List<Vector2Int> list)
    {
        for(int i = 0;i<list.Count;i++)
        {
            Logger.Log(list[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    Vector2Int Vector3ToInt(Vector3 vec)
    {
        Vector2Int toRet=Vector2Int.zero;
        toRet.x = ((int)vec.x);
        toRet.y = ((int)vec.y);
        return toRet;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTest : MonoBehaviour
{
    [SerializeField] TableWithProducts _table1;
    [SerializeField] TableWithProducts _table2;
    [SerializeField] AStarPathfinding _pathfinding;
    [SerializeField] List<Transform> _obstacles = new List<Transform>();
    List<List<Vector2Int>> lists=new List<List<Vector2Int>>();
    // Start is called before the first frame update
    void Start()
    {
        _pathfinding.SetObstacles(TransToListInt(_obstacles));
        _pathfinding.DivideMapIntoTiles();
        for(int i=0;i<_table2.AccessPoints.Count;i++)
        {
            _pathfinding.SetTargetTiles(Vector3ToInt(_table1.transform.position), Vector3ToInt(_table2.AccessPoints[i].position));
            _pathfinding.StartLooking();
        }
        List<Vector2Int> shorter = lists[0];
        for (int j=0;j< lists.Count;j++)
        {
            if (lists[j].Count < shorter.Count) shorter = lists[j];
            for(int k = 0; k < lists[j].Count;k++)
            {
                Logger.Log(lists[j][k]);
            }
            Logger.Log("New");
        }
        Logger.Log(shorter.Count);
    }
    public void DD(List<Vector2Int> list)
    {
        lists.Add(list);
        //for (int i = 0;i<list.Count;i++)
        //{
        //    Logger.Log(list[i]);
        //}
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
    List<Vector2Int> TransToListInt(List<Transform> trans)
    {
        List<Vector2Int> toRet = new List<Vector2Int>();
        for(int i=0;i< trans.Count;i++)
        {
            Vector2Int tmp = Vector2Int.zero;
            tmp.x = ((int)trans[i].position.x);
            tmp.y = ((int)trans[i].position.y);
            toRet.Add(tmp);
        }

        return toRet;
    }
}

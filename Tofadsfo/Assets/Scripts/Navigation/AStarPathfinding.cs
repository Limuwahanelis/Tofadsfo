using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AStarPathfinding : MonoBehaviour
{
    public UnityEvent<List<Vector2Int>> OnPathCreated;

   // [SerializeField] ObstaclePool _obstaclePool;
   // [SerializeField] TileObjectPlacer _tileObjectPlacer;
   // [SerializeField] TileMapSetter _tileMapSetter;

    [SerializeField]private Vector2Int _gridSize;

    private Vector2Int _startTilePos;
    private Vector2Int _endTilePos;

    private PathFindingNode _startNode;
    private PathFindingNode _endNode;

    private List<PathFindingNode> _open;
    private List<PathFindingNode> _close;
    private List<List<PathFindingNode>> _all;
    private List<Vector2Int> _obstaclesPos;
    private List<Vector2Int> _pathPoints;


    enum Direction
    {
        UP,RIGHT,DOWN,LEFT
    }
    public void SetObstacles()
    {
       // this._obstaclesPos = new List<Vector2Int>( _obstaclePool.GetAllActiveObstaclePositions());
    }
    public void SetTargetTiles()
    {
       // if (_tileObjectPlacer.IsGoalPlaced) _endTilePos =new Vector2Int( ((int)_tileObjectPlacer.GoalTile.transform.position.x), ((int)_tileObjectPlacer.GoalTile.transform.position.z));
       // if (_tileObjectPlacer.IsStartPlaced) _startTilePos = new Vector2Int(((int)_tileObjectPlacer.StartTile.transform.position.x), ((int)_tileObjectPlacer.StartTile.transform.position.z));
    }
    public void SetGridSize()
    {
        //_gridSize =new Vector2Int( ((int)_tileMapSetter.GridSize.x), ((int)_tileMapSetter.GridSize.y));
    }
    public void StartLooking()
    {
        //if (!_tileObjectPlacer.IsGoalPlaced) return;
        //if (!_tileObjectPlacer.IsStartPlaced) return;
            _open = new List<PathFindingNode>();
        _close = new List<PathFindingNode>();
        _startNode.gcost = 0;
        _startNode.hcost = Mathf.RoundToInt(Vector2.Distance(_startTilePos, _endTilePos) * 10);
        //startTile.fcost = startTile.gcost + 
        _open.Add(_startNode);
        PathFindingNode current = null;
        while (_open.Count>0)
        {
            current = FindNodeWithLowestFCost(_open);
            _open.Remove(current);
            _close.Add(current);

            if (current == _endNode) break;

            foreach (PathFindingNode node in current.neighbours)
            {
                if (node == null ||!node.traversable || _close.Exists((x) => x == node) ) continue;
                bool isNodeInOpen = _open.Exists((x) => x == node);
                if (!isNodeInOpen || current.gcost+Mathf.RoundToInt(Vector2.Distance(current.position, node.position) * 10)<node.gcost)
                {
                    node.VisitNode(current, _endNode);
                    if (!isNodeInOpen) _open.Add(node);
                }

            }
        }
        _pathPoints = new List<Vector2Int>();
        if (current == _endNode)
        {
            while (current != null)
            {
                _pathPoints.Add(current.position);
                current = current.previousNode;
            }
        }
        _pathPoints.Reverse();
        OnPathCreated?.Invoke(_pathPoints);
    }

    public void DivideMapIntoTiles()
    {
       // if (!_tileObjectPlacer.IsGoalPlaced) return;
       // if (!_tileObjectPlacer.IsStartPlaced) return;
        _all = new List<List<PathFindingNode>>();
        for (int i = 0;i<_gridSize.x;i++)
        {
            _all.Add(new List<PathFindingNode>());
            for(int j = 0;j<_gridSize.y;j++)
            {
                _all[i].Add(new PathFindingNode(i, j));
                if (i == _startTilePos.x && j == _startTilePos.y) _startNode = _all[i][j];
                if( i == _endTilePos.x &&j == _endTilePos.y) _endNode = _all[i][j];
                
                if (_obstaclesPos.Exists((vec) => vec.x == i && vec.y == j)) 
                {
                    Vector2Int obstaclepos = _obstaclesPos.Find((vec) => vec.x == i && vec.y == j);
                    _all[i][j].traversable = false;
                    _obstaclesPos.Remove(obstaclepos);
                }
            }
        }
        AssignNeighbours();
    }

    private void AssignNeighbours()
    {
        for (int i = 0; i < _all.Count; i++)
        {
            for(int j = 0; j < _all[i].Count; j++)
            {
                if (i == 0) _all[i][j].neighbours[((int)Direction.LEFT)] = null;
                else _all[i][j].neighbours[((int)Direction.LEFT)] = _all[i - 1][j];

                if(i== _all.Count-1) _all[i][j].neighbours[((int)Direction.RIGHT)] = null;
                else _all[i][j].neighbours[((int)Direction.RIGHT)] = _all[i+1][j];

                if(j==0) _all[i][j].neighbours[((int)Direction.DOWN)] = null;
                else _all[i][j].neighbours[((int)Direction.DOWN)] = _all[i][j-1];

                if(j == _all[i].Count - 1) _all[i][j].neighbours[((int)Direction.UP)] = null;
                else _all[i][j].neighbours[((int)Direction.UP)] = _all[i][j+1];
            }
        }
    }

    private PathFindingNode FindNodeWithLowestFCost(List<PathFindingNode> nodes)
    {
        PathFindingNode toReturn = nodes[0];
        int lowestFcost = nodes[0].fcost;

        for(int i=0;i<nodes.Count;i++) 
        {
            if (nodes[i].fcost < lowestFcost)
            {
                toReturn = nodes[i];
                lowestFcost = nodes[i].fcost;
            }
        }
        return toReturn;
    }

    public List<Vector2Int> GetPathPoints()
    {
        return _pathPoints;
    }

}

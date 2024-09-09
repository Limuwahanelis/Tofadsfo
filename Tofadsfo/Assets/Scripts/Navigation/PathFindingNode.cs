using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PathFindingNode
{
    public PathFindingNode(int x,int y)
    {
        position = new Vector2Int(x,y);
    }

    public Vector2Int position;
    public List<PathFindingNode> neighbours = new List<PathFindingNode>() {null,null,null,null };
    public PathFindingNode previousNode;
    public bool traversable = true;

    public int gcost;
    public int hcost;
    public int fcost;

    public void VisitNode(PathFindingNode currentNode, PathFindingNode end)
    {
        gcost = currentNode.gcost + Mathf.RoundToInt(Vector2.Distance(currentNode.position,position) * 10);
        hcost = Mathf.RoundToInt( Vector2.Distance(position, end.position)*10);
        fcost = gcost + hcost;
        previousNode = currentNode;
        //gcost 
    }
}

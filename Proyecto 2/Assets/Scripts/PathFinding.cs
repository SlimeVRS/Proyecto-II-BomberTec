using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private MapManager _grid;
    private LinkedList _openList;
    private LinkedList _closedList;
    private const int MovementCost = 10;
    
    

    public PathFinding()
    {
       
    }

    public void SetMap(MapManager gridMap)
    {
        this._grid = gridMap;
    }

    private LinkedList GetObstacles()
    {
        LinkedList obstacleList = new LinkedList();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (_grid.GetPathNode(x, y).isObstacle)
                {
                    obstacleList.AddNode(_grid.GetPathNode(x, y));
                }
            }
        }

        return obstacleList;
    }

    public LinkedList FindPath(int startX, int startY, int endX, int endY)
    {
        Node startNode = _grid.GetPathNode(startX, startY);
        Node endNode = _grid.GetPathNode(endX, endY);
        _openList = new LinkedList();
        _openList.AddNode(startNode);
        _closedList = GetObstacles();

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Node node = _grid.GetPathNode(x, y);
                node.SetGCost(int.MaxValue);
                node.CalculateFCost();
                node.cameFrom = null;
            }
        }
        
        startNode.SetGCost(0);
        int hCost = CalculateDistance(startNode, endNode);
        startNode.SetHCost(hCost);
        startNode.CalculateFCost();

        while (_openList.length > 0)
        {
            Node currentNode = LowestFCost(_openList);
            if (currentNode == endNode)
            {
                return CalculatedPath(endNode);
            }
            
            _openList.RemoveNode(currentNode);
            _closedList.AddNode(currentNode);

            for (Node neighbour = GetNeighbours(currentNode).GetHead();
                neighbour != null;
                neighbour = neighbour.GetNextNode())
            {
                if(_closedList.Contains(neighbour)) continue;

                int tempGCost = currentNode.GetGCost() + CalculateDistance(currentNode, neighbour);
                if (tempGCost < neighbour.GetGCost())
                {
                    neighbour.cameFrom = currentNode;
                    neighbour.SetGCost(tempGCost);
                    neighbour.SetHCost(CalculateDistance(neighbour, endNode));
                    neighbour.CalculateFCost();

                    if (!_openList.Contains(neighbour))
                    {
                        _openList.AddNode(neighbour);
                    }
                }
            }
            
            
        }

        return null;

    }

    private LinkedList GetNeighbours(Node currentNode)
    {
        LinkedList neighbourList =  new LinkedList();

        if (currentNode.GetX() - 1 >= 0)
        {
            //left
            neighbourList.AddNode(_grid.GetPathNode(currentNode.GetX()-1,currentNode.GetY()));
        }

        if (currentNode.GetX() + 1 < 10)
        {
            //right
            neighbourList.AddNode(_grid.GetPathNode(currentNode.GetX() + 1, currentNode.GetY()));
        }

        if (currentNode.GetY() - 1 >= 0)
        {
            //Down
            neighbourList.AddNode(_grid.GetPathNode(currentNode.GetX(),currentNode.GetY()-1));
        }

        if (currentNode.GetY() + 1 < 10)
        {
            //up
            neighbourList.AddNode(_grid.GetPathNode(currentNode.GetX(),currentNode.GetY()+1));
        }

        return neighbourList;

    }
    
    private LinkedList CalculatedPath(Node destination)
    {

       LinkedList path = new LinkedList();
       path.AddNode(destination);
       Node currentNode = destination;
       while (currentNode.cameFrom != null)
       {
           path.AddNode(currentNode.cameFrom);
           currentNode = currentNode.cameFrom;
       }

       path = path.Reverse();
       
       return path;
    }

    private Node LowestFCost(LinkedList openList)
    {
        Node lowestFNode = openList.GetHead();
        for (Node node = openList.GetHead(); node != null; node = node.GetNextNode())
        {
            if (node.GetFCost() < lowestFNode.GetFCost())
            {
                lowestFNode = node;
            }
        }

        return lowestFNode;
    }

    private int CalculateDistance(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        return MovementCost * (xDistance + yDistance);
    }
}

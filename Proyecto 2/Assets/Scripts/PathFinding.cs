using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A pathfinding class that implements the A* algorithm.
/// </summary>
public class PathFinding
{
    private MapManager _grid;
    private int _mapSize;
    private List<Node> _openList;
    private List<Node> _closedList;
    private const int MovementCost = 10;
    private List<Node> pathFound;
    
    

    /// <summary>
    /// Default constructor
    /// </summary>
    public PathFinding()
    {
       
    }
    
    /// <summary>
    /// A debugging function that prints a path of nodes.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        String pathString = " ";
        for (int i = 0; i < pathFound.Count; i++)
        {
            pathString += pathFound[i].GetX() + " " + pathFound[i].GetY() + " ||| ";
        }

        return pathString;
    }

    /// <summary>
    /// Sets the MapManager object to be used in order to create a path.
    /// </summary>
    /// <param name="gridMap"></param>
    public void SetMap(MapManager gridMap)
    {
        this._grid = gridMap;
        _mapSize = this._grid._mapSize;

    }

    private List<Node> GetObstacles()
    {
        var obstacleList = new List<Node>();
        for (int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                Node gridNode = _grid.GetPathNode(x, y);
                if (gridNode.isObstacle)
                {
                    obstacleList.Add(gridNode);
                }
            }
        }

        return obstacleList;
    }

    /// <summary>
    /// Finds the path between two given sets of coordinates using A* algorithm
    /// </summary>
    /// <param name="startX"> An integer that represents the x position of the starting node</param>
    /// <param name="startY"> An integer that represents the y position of the starting node</param>
    /// <param name="endX"> An integer that represents the x position of the end node</param>
    /// <param name="endY"> An integer that represents the y position of the end node</param>
    /// <returns> A list of nodes that represent the path to take</returns>
    public List<Node> FindPath(int startX, int startY, int endX, int endY)
        {
            Node startNode = _grid.GetPathNode(startX, startY);
            Node endNode = _grid.GetPathNode(endX, endY);
            _openList = new List<Node> {startNode};
            _closedList = GetObstacles();

            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
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

            while (_openList.Count > 0)
            {
                Node currentNode = LowestFCost(_openList);
                /*Debug.Log("CURRENT NODE: "+currentNode.GetX()+", "+currentNode.GetY());
                Debug.Log("END NODE: "+endNode.GetX()+", "+endNode.GetY());*/
                if (currentNode == endNode)
                {
                    pathFound = CalculatedPath(endNode);
                    /*Debug.Log(ToString());*/
                    return pathFound;
                }
                _openList.Remove(currentNode);
                _closedList.Add(currentNode);
                
                List<Node> neighbours = GetNeighbours(currentNode);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    Node neighbour = neighbours[i];
                    if (_closedList.Contains(neighbour)) continue;

                    int tempGCost = currentNode.GetGCost() + CalculateDistance(currentNode, neighbour);
                    if (tempGCost < neighbour.GetGCost())
                    {
                        neighbour.cameFrom = currentNode;
                        neighbour.SetGCost(tempGCost);
                        neighbour.SetHCost(CalculateDistance(neighbour, endNode));
                        neighbour.CalculateFCost();

                        if (!_openList.Contains(neighbour))
                        {
                            _openList.Add(neighbour);
                        }
                    }
                }


            }
            Debug.Log("NO PATH");
            return null;

        }

    private List<Node> GetNeighbours(Node currentNode)
    {
        var neighbourList = new List<Node>();

        if (currentNode.GetX() - 1 >= 0)
        {
            //left
            neighbourList.Add(_grid.GetPathNode(currentNode.GetX() - 1, currentNode.GetY()));
        }

        if (currentNode.GetX() + 1 < _mapSize)
        {
            //right
            neighbourList.Add(_grid.GetPathNode(currentNode.GetX() + 1, currentNode.GetY()));
        }

        if (currentNode.GetY() - 1 >= 0)
        {
            //Down
            neighbourList.Add(_grid.GetPathNode(currentNode.GetX(), currentNode.GetY() - 1));
        }

        if (currentNode.GetY() + 1 < _mapSize)
        {
            //up
            neighbourList.Add(_grid.GetPathNode(currentNode.GetX(), currentNode.GetY() + 1));
        }
        return neighbourList;

    }
    
    private List<Node> CalculatedPath(Node destination)
    {

        List<Node> path = new List<Node>();
        path.Add(destination);
        Node currentNode = destination;
        while (currentNode.cameFrom != null)
        {
            path.Add(currentNode.cameFrom);
            currentNode = currentNode.cameFrom;
        }

        path.Reverse();
        return path;
    }

    private Node LowestFCost(List<Node> openList)
    {
        Node lowestFNode = openList[0];
        for (var i = 1; i < openList.Count; i++)
        {
            if (openList[i].GetFCost() < lowestFNode.GetFCost())
            {
                lowestFNode = openList[i];
            }
        }

        return lowestFNode;
    }

    private int CalculateDistance(Node a, Node b)
    {
        int xDistance = Math.Abs(a.GetX() - b.GetX());
        int yDistance = Math.Abs(a.GetY() - b.GetY());
        return MovementCost * (xDistance + yDistance);
    }
}

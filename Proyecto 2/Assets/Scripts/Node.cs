using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Node class used for pathfinding purposes, stores the relevant information for the A* algorithm execution.
/// </summary>
public class Node
{

    private int _x;
    private int _y;
    private int _gCost;
    private int _hCost;
    private int _fCost;
    /// <summary>
    /// A flag that determines if the cell corresponds to an obstacle in the map
    /// </summary>
    public bool isObstacle = false;
    /// <summary>
    /// A reference to the next Node object.
    /// </summary>
    public Node cameFrom = null;

    /// <summary>
    /// Public constructor of the class
    /// </summary>
    /// <param name="x"> The x coordinate of the pathfinding map matrix in which the node is to be set</param>
    /// <param name="y"> The y coordinate of the pathfinding map matrix in which the node is to be set</param>
    public Node(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    /// <summary>
    /// Gets the X coordinate of the pathfinding map in which the node resides
    /// </summary>
    /// <returns> An integer that represents a row index on the pathfinding map</returns>
    public int GetX()
    {
        return this._x;
    }

    /// <summary>
    /// Gets the Y coordinate of the pathfinding map in which the node resides
    /// </summary>
    /// <returns> An integer that represents a column index on the pathfinding map</returns>
    public int GetY()
    {
        return this._y;
    }


    /// <summary>
    /// Gets the set G cost of the Node
    /// </summary>
    /// <returns> An integer that represent the G cost</returns>
    public int GetGCost()
    {
        return _gCost;
    }

    /// <summary>
    /// Sets the set G cost of the Node
    /// </summary>
    public void SetGCost(int gCost)
    {
        this._gCost = gCost;
    }

    /// <summary>
    /// Gets the set H cost of the Node
    /// </summary>
    /// <returns> An integer that represent the H cost</returns>
    public int GetHCost()
    {
        return _hCost;
    }

    /// <summary>
    /// Sets the set H cost of the Node
    /// </summary>
    public void SetHCost(int hCost)
    {
        this._hCost = hCost;
    }

    /// <summary>
    /// Gets the set F cost of the Node
    /// </summary>
    /// <returns> An integer that represent the F cost</returns>
    public int GetFCost()
    {
        return this._fCost;
    }

    /// <summary>
    /// A debugging function that prints the x and y coordinates of a given Node
    /// </summary>
    /// <returns> A string xy coordinate</returns>
    public override string ToString()
    {
        return _x + ", " + _y;
    }

    /// <summary>
    /// Calculates the F cost using the sum of the g and h values
    /// </summary>
    public void CalculateFCost()
    {
        this._fCost = _gCost + _hCost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    private int _x;
    private int _y;
    private int _gCost;
    private int _hCost;
    private int _fCost;
    public bool isObstacle = false;
    public Node cameFrom = null;

    /// <sumary>
    /// Create a new node with coordinates
    /// </sumary>
    /// <param name="x"> Coordinate x</param>
    /// <param name="y"> Coordinate y</param>
    public Node(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    /// <sumary>
    /// Returns the x coordinate of the node
    /// </sumary>
    public int GetX()
    {
        return this._x;
    }

    /// <sumary>
    /// Returns the y coordinate of the node
    /// </sumary>
    public int GetY()
    {
        return this._y;
    }

    /// <sumary>
    /// Returns the G cost of the node
    /// </sumary>
    public int GetGCost()
    {
        return _gCost;
    }

    /// <sumary>
    /// Sets the G cost of the node
    /// </sumary>
    /// <param name="gCost">New G cost of the node</param>
    public void SetGCost(int gCost)
    {
        this._gCost = gCost;
    }

    /// <sumary>
    /// Returns the H cost of the node
    /// </sumary>
    public int GetHCost()
    {
        return _hCost;
    }

    /// <sumary>
    /// Sets the H cost of the node
    /// </sumary>
    /// <param name="hCost">New G cost of the node</param>
    public void SetHCost(int hCost)
    {
        this._hCost = hCost;
    }

    /// <sumary>
    /// Returns the F cost of the node
    /// </sumary>
    public int GetFCost()
    {
        return this._fCost;
    }


    /// <sumary>
    /// Gives the coordinates of the node in a string type
    /// </sumary>
    public override string ToString()
    {
        return _x + ", " + _y;
    }


    /// <sumary>
    /// Calculate the F cost of the node
    /// </sumary>
    public void CalculateFCost()
    {
        this._fCost = _gCost + _hCost;
    }
}

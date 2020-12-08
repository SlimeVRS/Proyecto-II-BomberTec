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

    public Node(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public int GetX()
    {
        return this._x;
    }

    public int GetY()
    {
        return this._y;
    }


    public int GetGCost()
    {
        return _gCost;
    }

    public void SetGCost(int gCost)
    {
        this._gCost = gCost;
    }

    public int GetHCost()
    {
        return _hCost;
    }

    public void SetHCost(int hCost)
    {
        this._hCost = hCost;
    }

    public int GetFCost()
    {
        return this._fCost;
    }

    public override string ToString()
    {
        return _x + ", " + _y;
    }

    public void CalculateFCost()
    {
        this._fCost = _gCost + _hCost;
    }
}

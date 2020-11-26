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
    private Node _previousNode;
    private Node _nextNode;

    public Node(int x, int y)
    {
        this._x = x;
        this._y = y;
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

    public Node GetPreviousNode()
    {
        return _previousNode;
    }

    public Node GetNextNode()
    {
        return _nextNode;
    }

    public void SetPreviousNode(Node previous)
    {
        this._previousNode = previous;
    }

    public void SetNextNode(Node next)
    {
        this._nextNode = next;
    }

    public override string ToString()
    {
        return _x + ", " + _y;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile 
{
    private int tileType;
    private bool isVisited;
    private int[] position;

    public MapTile(int id, int i, int j)
    {
        tileType = id;
        position = new int[2];
        position[0] = i;
        position[1] = j;
    }

    public int TileType
    {
        get => tileType;
        set => tileType = value;
    }

    public bool IsVisited
    {
        get => isVisited;
        set => isVisited = value;
    }

    public int[] Position => position;
}


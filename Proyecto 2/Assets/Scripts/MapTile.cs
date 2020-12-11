using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that contains information of an specific tile in a map, used in the backtracking map
/// </summary>
public class MapTile 
{
    private int tileType;
    private bool isVisited;
    private int[] position;

    /// <summary>
    /// Constructor for the class
    /// </summary>
    /// <param name="id"> A numeric identificator for the MapTile object</param>
    /// <param name="i"> An integer that represents the x coordinate of the tile in the map</param>
    /// <param name="j"> An integer that represents the y coordinate of the tile in the map</param>
    public MapTile(int id, int i, int j)
    {
        tileType = id;
        position = new int[2];
        position[0] = i;
        position[1] = j;
    }

    /// <summary>
    /// Sets and get the type of tile that will be represented by the object. Entries can be 0 for free tile, 1 for destructible tile, 2 for indestructible tile
    /// </summary>
    public int TileType
    {
        get => tileType;
        set => tileType = value;
    }

    /// <summary>
    /// Sets if the tile is visited or not
    /// </summary>
    public bool IsVisited
    {
        get => isVisited;
        set => isVisited = value;
    }

    /// <summary>
    /// Sets the position of the tile.
    /// </summary>
    public int[] Position => position;
}


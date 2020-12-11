using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node class used for the logic map construction and backtracking algorithm, it contains a reference to another node
/// and a reference to a MapTile object, which holds information about a specific map position.
/// </summary>
public class BackTrackingNode
{
    
    private BackTrackingNode nextNode;
    private MapTile tile;

    /// <summary>
    /// Constructor for the class, takes a MapTile as an argument and sets it to a private atribute.
    /// </summary>
    /// <param name="tile"></param>
    public BackTrackingNode(MapTile tile)
    {
        this.tile = tile;
        nextNode = null;
    }

    /// <summary>
    /// A public propierty that sets the reference to the next node if given a BackTracking node object as an argument,
    /// alternatively, if called with no argument, returns the reference to the next node if no argument is given.
    /// </summary>
    public BackTrackingNode NextNode
    {
        get => nextNode;
        set => nextNode = value;
    }

    /// <summary>
    /// A public propierty that sets the reference to the to the MapTile if given a MapTile object as an argument,
    /// alternatively, if called with no argument, returns the reference to the contained MapTile if no argument is given.
    /// </summary>
    public MapTile Tile
    {
        get => tile;
        set => tile = value;
    }
}

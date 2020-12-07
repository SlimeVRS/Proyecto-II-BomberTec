using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrackingNode
{
    private BackTrackingNode nextNode;
    private MapTile tile;

    public BackTrackingNode(MapTile tile)
    {
        this.tile = tile;
        nextNode = null;
    }

    public BackTrackingNode NextNode
    {
        get => nextNode;
        set => nextNode = value;
    }

    public MapTile Tile
    {
        get => tile;
        set => tile = value;
    }
}

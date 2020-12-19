using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using CodeMonkey.Utils;
using Object = UnityEngine.Object;
using Random = System.Random;

/// <summary>
/// The MapManager class handles all map related functions, from map generation to renderization on screen. It creates logical maps that handle pathfinding and backtracking algorithms that
/// were requested for the project.
/// </summary>
public class MapManager
{
    private int _height;
    private int _width;
    private float _cellSize;
    private int[,] mapArray;
    private readonly MapTile[,] _backtrackingMap;
    /// <summary>
    /// The size of the matrix that represents the logical map.
    /// </summary>
    public readonly int _mapSize;
    private Node[,] _pathFinding;
    private GameObject destructible;
    private GameObject indestructible;
    private bool isConstructing;



    /// <summary>
    /// A getter for the logical map used to perform the pathfinding algorithm used in the game.
    /// </summary>
    /// <returns>A reference to a 11x11 matrix filled with Node objects</returns>
    public Node[,] GetPathfindingMap()
    {
        return _pathFinding;
    }

    
    private void SetPathFindingMap()
    {
        _pathFinding = new Node[_height, _width];
        for (int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                Node node = new Node(x, y);
                if (_backtrackingMap[x, y].TileType == 2)
                {
                    node.isObstacle = true;
                }

                _pathFinding[x, y] = node;
            }
        }
    }

    /// <summary>
    /// Given a specific x y coordinate, returns a Node with information related to pathfinding data.
    /// </summary>
    /// <param name="x"> The x coordinate of the desired pathfinding map Node</param>
    /// <param name="y"> The y coordinate of the desired pathfinding map Node</param>
    /// <returns> The pathfinding Node on the specified x y position of the pathfinding map matrix</returns>
    public Node GetPathNode(int x, int y)
    {
        return _pathFinding[x, y];
    }


    /// <summary>
    /// A constructor and initializer for the MapManager class, creates the instance of an MapManager object and
    /// initializes the basic attributes it needs to proceed with logical processes related to map management, both
    /// visual and logical.
    /// </summary>
    /// <param name="cellSize"> The float size of the cells that will be drawn on screen as map tiles</param>
    /// <param name="destructible"> A reference to the unity prefab that represents the destructible blocks in screen</param>
    /// <param name="indestructible">A reference to the unity prefab that represents the destructible blocks in screen</param>
    public MapManager(float cellSize, GameObject destructible, GameObject indestructible, bool generation)
    {
        this._width = 11;
        this._height = 11;
        this._mapSize = 11;
        this._cellSize = cellSize;
        this.destructible = destructible;
        this.indestructible = indestructible;
        isConstructing = generation;
        mapArray = new int[_width, _height];
        _backtrackingMap = new MapTile[_width, _height];
        CreateDefaultMap();
        RandomizeMap();
        SetSpawners();
        SetPathFindingMap();
        _GenerateStaticMap();
        
        /*Debug.Log(ToString());*/


        for (int x = 0; x < mapArray.GetLength(0); x++)
        {
            for (int y = 0; y < mapArray.GetLength(1); y++)
            {
                /*UtilsClass.CreateWorldText("(" + x + "," + y + ")", null,
                    GetWorldPosition(x, y) + new Vector2(_cellSize, _cellSize) * 0.5f, 30, Color.white,
                    TextAnchor.MiddleCenter);*/
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 1000f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 1000f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 1000f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 1000f);
        

    }

    private void SetSpawners()
    {
        _backtrackingMap[0, 10].TileType = 0;
        _backtrackingMap[0, 9].TileType = 0;
        _backtrackingMap[1, 10].TileType = 0;
        _backtrackingMap[0, 1].TileType = 0;
        _backtrackingMap[0, 0].TileType = 0;
        _backtrackingMap[1, 0].TileType = 0;
        _backtrackingMap[9, 10].TileType = 0;
        _backtrackingMap[10, 10].TileType = 0;
        _backtrackingMap[10, 9].TileType = 0;
        _backtrackingMap[10, 1].TileType = 0;
        _backtrackingMap[10, 0].TileType = 0;
        _backtrackingMap[9, 0].TileType = 0;
        _backtrackingMap[8, 5].TileType = 0;
        _backtrackingMap[2, 5].TileType = 0;
        _backtrackingMap[5, 7].TileType = 0;
        _backtrackingMap[5, 3].TileType = 0;
    }

    private void CreateDefaultMap()
    {
        for (var i = 0; i < _mapSize; i++)
        {
            for (var j = 0; j < _mapSize; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                {
                    _backtrackingMap[i,j] = new MapTile(0,i,j);
                }else if (i % 2 == 0 && j % 2 != 0)
                {
                    _backtrackingMap[i,j] = new MapTile(2,i,j);
                }
                else
                {
                    _backtrackingMap[i,j] = new MapTile(2,i,j );
                }
                
            }
        }
    }

    /// <summary>
    /// The backtracking algorthim used to create a random map each time the game is executed. The never has closed rooms.
    /// It's a modification of the depth first backtracker algorithm which essentially creates a random maze making use of
    /// a Stack data structure to backtrack whenever a condition set by the programmer is not met.
    /// </summary>
    public void RandomizeMap()
    {
        Stack backtrackingStack = new Stack();
        backtrackingStack.push(new BackTrackingNode(_backtrackingMap[0, 0]));
        _backtrackingMap[0, 0].IsVisited = true;
        for (BackTrackingNode currentNode = backtrackingStack.peek();
            currentNode != null;
            currentNode = backtrackingStack.peek())
        {
            List<MapTile> randomNeighbour = GetRandomNeighbour(currentNode.Tile);

            if (randomNeighbour == null)
            {
                backtrackingStack.pop();
            }
            else
            {
                randomNeighbour[0].TileType = UnityEngine.Random.Range(0, 2);
                randomNeighbour[1].TileType = UnityEngine.Random.Range(0, 2);
                randomNeighbour[1].IsVisited = true;
                backtrackingStack.push(new BackTrackingNode(randomNeighbour[1]));
            }
        }
    }

    private List<MapTile> GetRandomNeighbour(MapTile currentTile)
    {
        var position = currentTile.Position;
        var allPossibleNeighbours = GetAllNeighbours(position);
        if (allPossibleNeighbours.Count == 0) return null;
        return allPossibleNeighbours[UnityEngine.Random.Range(0, allPossibleNeighbours.Count)];
    }

    private List<List<MapTile>> GetAllNeighbours(int[] currentTilePosition)
    {
        var allNeighbours = new List<List<MapTile>>();

        var i = currentTilePosition[0];
        var j = currentTilePosition[1];

        if (ValidateDirection(i, j, -1, 0))
            allNeighbours.Add(GetNeighboursInDirection(i, j, -1, 0));

        if (ValidateDirection(i, j, 0, 1))
            allNeighbours.Add(GetNeighboursInDirection(i, j, 0, 1));

        if (ValidateDirection(i, j, 1, 0))
            allNeighbours.Add(GetNeighboursInDirection(i, j, 1, 0));

        if (ValidateDirection(i, j, 0, -1))
            allNeighbours.Add(GetNeighboursInDirection(i, j, 0, -1));

        return allNeighbours;
    }

    private bool ValidateDirection(int iCurrent, int jCurrent, int iDir, int jDir)
    {
        if (iDir != 0)
        {
            if ((iCurrent + iDir * 2) >= 0 && (iCurrent + iDir * 2) < _mapSize)
            {
                if (_backtrackingMap[iCurrent + iDir * 2, jCurrent].IsVisited)
                    return false;
            }
            else
                return false;

        }
        else
        {
            if ((jCurrent + jDir * 2) >= 0 && (jCurrent + jDir * 2) < _mapSize)
            {
                if (_backtrackingMap[iCurrent, jCurrent + jDir * 2].IsVisited)
                    return false;
            }
            else
                return false;
        }

        return true;
    }

    private List<MapTile> GetNeighboursInDirection(int iCurrent, int jCurrent, int iDir, int jDir)
    {

        var neighbours = new List<MapTile>
        {
            _backtrackingMap[iCurrent + iDir, jCurrent + jDir],
            _backtrackingMap[iCurrent + iDir * 2, jCurrent + jDir * 2]
        };
        return neighbours;
    }

    private int RandomIntInRange(int x, int y)
    {
        var rnd = new Random();
        var randomInt = rnd.Next(x, y);
        return randomInt;
    }

    /// <summary>
    /// Converts an x,y position of the logical map into a screen position in the unity scene.
    /// </summary>
    /// <param name="x"> Integer that represents the x coordinate of the logical map</param>
    /// <param name="y">Integer that represents the y coordinate of the logical map</param>
    /// <returns> A Vector2 object, a two dimensional vector that contains the screen coordinates equivalent to the logical x,y position in the map matrix</returns>
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize;
    }

    /// <summary>
    /// Converts an Vector2 screen position of unity scene into a logical x,y position in the logical map matrix.
    /// </summary>
    /// <param name="worldPosition"> A vector2 object, a two dimensional vector that holds the coordinates of a screen position in the unity scene </param>
    /// <returns>  The logical x,y position in the map matrix equivalent to the Vector2 coordinate on the unity scene</returns>
    public Vector2Int GetMatrixPosition(Vector2 worldPosition)
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        matrixPosition.y = Mathf.FloorToInt(worldPosition.y / _cellSize);
        return matrixPosition;
    }

    private void _placeBlock(int x, int y, GameObject block)
    {
        Object.Instantiate(block, GetWorldPosition(x, y) + new Vector2(_cellSize, _cellSize) * 0.5f,
            Quaternion.identity);
    }
    
    /// <summary>
    /// A debugging function that prints in console data of the generated random map
    /// </summary>
    /// <returns> A String containing information about the backtracking generated random map</returns>
    public override string ToString()
    {
        var mapStr = "";
        for (int i = 0; i < _mapSize; i++)
        {
            var row = "";
            for (int j = 0; j < _mapSize; j++)
            {
                row += _backtrackingMap[i, j].TileType + "  ";
            }
            mapStr += row + "\n";
        }

        return mapStr;
    }

    private void _GenerateStaticMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                if (_backtrackingMap[x, y].TileType == 1 && isConstructing)
                {
                    _placeBlock(x, y, destructible);
                }

                if (_backtrackingMap[x, y].TileType == 2)
                {
                    _placeBlock(x, y, indestructible);
                }
            }
        }
    }

};

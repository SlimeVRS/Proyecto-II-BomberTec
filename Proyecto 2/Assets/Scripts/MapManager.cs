using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using CodeMonkey.Utils;
using Object = UnityEngine.Object;
using Random = System.Random;

public class MapManager
{
    private int _height;
    private int _width;
    private float _cellSize;
    private int[,] mapArray;
    private readonly MapTile[,] _backtrackingMap;
    public readonly int _mapSize;
    private Node[,] _pathFinding;
    private GameObject destructible;
    private GameObject indestructible;

    

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

    public Node GetPathNode(int x, int y)
    {
        return _pathFinding[x, y];
    }


    public MapManager(float cellSize, GameObject destructible, GameObject indestructible)
    {
        this._width = 11;
        this._height = 11;
        this._mapSize = 11;
        this._cellSize = cellSize;
        this.destructible = destructible;
        this.indestructible = indestructible;
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
                UtilsClass.CreateWorldText("(" + x + "," + y + ")", null,
                    GetWorldPosition(x, y) + new Vector2(_cellSize, _cellSize) * 0.5f, 30, Color.white,
                    TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
        

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

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize;
    }

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
                if (_backtrackingMap[x, y].TileType == 1)
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

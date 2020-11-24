using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using CodeMonkey.Utils;

public class MapManager
{
    private int _height;
    private int _width;
    private float _cellSize;
    private int[,] mapArray;
    private int[,] _staticMap;
    private Node[,] pathFinding;
    private GameObject destructible;
    private GameObject indestructible;



    private void SetStaticMap()
    {
        _staticMap = new int[10, 10]
        {
            {0, 0, 1, 0, 1, 1, 1, 1, 0, 0},
            {0, 2, 0, 1, 2, 1, 0, 1, 1, 0},
            {1, 1, 1, 0, 0, 0, 0, 2, 0, 1},
            {0, 1, 0, 1, 1, 0, 1, 0, 0, 0},
            {1, 0, 2, 2, 0, 1, 2, 1, 0, 1},
            {0, 1, 0, 2, 1, 0, 0, 0, 2, 1},
            {1, 0, 1, 0, 0, 0, 1, 0, 0, 1},
            {2, 0, 1, 0, 1, 1, 0, 1, 1, 1},
            {0, 1, 1, 2, 0, 1, 1, 2, 1, 0},
            {0, 0, 1, 1, 1, 2, 0, 1, 0, 0}
            
        };
    }
    public MapManager(int width, int height, float cellSize,GameObject destructible, GameObject indestructible)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this.destructible = destructible;
        this.indestructible = indestructible;
        mapArray = new int[width,height];
        SetStaticMap();
        

        for (int x = 0; x < mapArray.GetLength(0); x++)
        {
            for (int y = 0; y < mapArray.GetLength(1); y++)
            {
                UtilsClass.CreateWorldText("("+x+","+y+")", null, GetWorldPosition(x,y)+new Vector3(_cellSize,_cellSize) * 0.5f,30,Color.white,TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x,y+1),Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y),GetWorldPosition(x+1,y),Color.white,100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,100f);
        Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white,100f);
        _GenerateStaticMap();
        
        
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,y)*_cellSize;
    }

    private Vector2Int GetMatrixPosition(Vector3 worldPosition)
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        matrixPosition.y = Mathf.FloorToInt(worldPosition.y / _cellSize);
        return matrixPosition;
    }

    private void _placeBlock(int x, int y, GameObject block)
    {
        Object.Instantiate(block, GetWorldPosition(x, y)+new Vector3(_cellSize,_cellSize) * 0.5f, Quaternion.identity);
    }

    private void _GenerateStaticMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (_staticMap[x,y] == 1)
                {
                    _placeBlock(x,y,destructible);
                }

                if (_staticMap[x, y] == 2)
                {
                    _placeBlock(x,y,indestructible);
                }
            }
        }
    }

}

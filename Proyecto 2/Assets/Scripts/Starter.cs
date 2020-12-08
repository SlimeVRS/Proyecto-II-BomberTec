using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Starter : MonoBehaviour
{
    private MapManager _map;

    [SerializeField] private GameObject destructible;

    [SerializeField] private GameObject indestructible;

    [SerializeField] private GameObject botController;

    private GeneticController _geneticController;

    private float _cellSize;

    // Start is called before the first frame update
    void Start()
    {
        _cellSize = 10f;
        _map = new MapManager( _cellSize, destructible,indestructible);
        _geneticController = GameObject.Instantiate(botController).GetComponent<GeneticController>();
        _geneticController.SetMap(_map);

    }

    

    // Update is called once per frame
    void Update()
    {

    }
}

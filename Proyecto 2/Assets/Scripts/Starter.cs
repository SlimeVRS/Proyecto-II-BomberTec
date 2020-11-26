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
    
    [SerializeField] private GameObject playerPrefab;

    private GeneticController _geneticController;

    private CharacterManager _player1;
    
    private float _cellSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _cellSize = 10f;
        _map = new MapManager(10,10, _cellSize, destructible,indestructible);
        _geneticController = GameObject.Instantiate(botController).GetComponent<GeneticController>();
        _geneticController.SetMap(_map);
        _SpawnPlayer();

    }

    private void _SpawnPlayer()
    {
        Vector2 playerSpawn = _map.GetWorldPosition(0, 9)+new Vector2(_cellSize,_cellSize) * 0.5f;
        GameObject player = GameObject.Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        _player1 = player.GetComponent<CharacterManager>();

    }
    

    // Update is called once per frame
    void Update()
    {

    }
}

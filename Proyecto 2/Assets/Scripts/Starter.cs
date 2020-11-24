using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private MapManager _map;
        
    [SerializeField] private GameObject destructible;

    [SerializeField] private GameObject indestructible;

    private GeneticController _botController;
    
    [SerializeField] private GameObject player1;
    
    // Start is called before the first frame update
    void Start()
    {
        _map = new MapManager(10,10, 10f, destructible,indestructible);
        _SpawnPlayer();
        
    }

    private void _SpawnPlayer()
    {
        Vector2 playerSpawn = _map.GetWorldPosition(0, 9)+new Vector2(10f,10f) * 0.5f;
        GameObject.Instantiate(player1, playerSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

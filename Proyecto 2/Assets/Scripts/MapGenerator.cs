using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    private int[,] _map = new int[10,10];
    // Start is called before the first frame update
    void Start()
    {
        _map[0,0] = 1;
        Debug.Log(_map[0,0]);
        Debug.Log(_map[0,1]);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

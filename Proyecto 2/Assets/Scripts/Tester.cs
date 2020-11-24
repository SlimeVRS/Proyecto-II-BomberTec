using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private GameObject destructible;

    [SerializeField] private GameObject indestructible;
    // Start is called before the first frame update
    void Start()
    {
        MapManager map = new MapManager(10,10, 10f, destructible,indestructible);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

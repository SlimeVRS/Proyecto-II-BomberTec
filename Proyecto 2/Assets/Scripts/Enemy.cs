using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private Rigidbody2D _enemyBody;

    public int health;
    public int speed;
    public int radius;
    public int[] actionProbability;
    private bool _isActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void OnEnable()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        actionProbability = new int[10];
    }

    public void Spawn(float xPosition)
    {
        
        _enemyBody.position = new Vector2(xPosition,0);
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

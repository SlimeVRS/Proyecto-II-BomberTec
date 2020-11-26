﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D _enemyBody;
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
    

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D _enemyBody;
    public int health;
    public int currentHealth;
    public int speed;
    public int radius;
    public int proximity;
    public int hitScore;
    public int[] actionProbability;
    private bool _isActive = false;
    public Vector2Int _botMatrixPos;

    public StatusBar statusBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        statusBar.gameObject.SetActive(currentHealth<=health);
        statusBar.SetMaxHealth(health);
        statusBar.SetHealth(currentHealth);
    }

    private void OnEnable()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        actionProbability = new int[10];
    }
    
    public void GetBotPosition()
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(_enemyBody.position.x / 10f);
        matrixPosition.y = Mathf.FloorToInt(_enemyBody.position.y / 10f);
        _botMatrixPos = matrixPosition;

    }
    
    

    // Update is called once per frame
    void Update()
    {
        GetBotPosition();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D _enemyBody;
    public int health;
    public int speed;
    public int radius;
    public int proximity;
    public int hitScore;
    public int[] actionProbability;
    private bool _isActive = false;
    public Vector2Int _botMatrixPos;
    private List<Vector3> pathVectorList = new List<Vector3>();
    private int currentNode;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentNode = 0;
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

    public void MoveBot(List<Node> path)
    {
        
        Vector3 target = new Vector3(path[1].GetX(),path[1].GetY(),0f) * 10f + new Vector3(10f, 10f,0f) * 0.5f;
        Debug.Log("TARGET: "+target.x+", "+target.y);
        ChangeTransform(target);
    }

    private void ChangeTransform(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position,target,10*Time.deltaTime);
    }
    

    // Update is called once per frame
    void Update()
    {
        
        GetBotPosition();
    }
}

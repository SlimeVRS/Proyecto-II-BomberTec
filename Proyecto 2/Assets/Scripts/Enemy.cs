using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// The controller class for the enemy AI, manages the different actions, data and components 
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The unity component that manages physics on the 2D plane scene.
    /// </summary>
    public Rigidbody2D _enemyBody;
    /// <summary>
    /// An integer that represents the health of the bot
    /// </summary>
    public int health;
    /// <summary>
    /// An integer that represents the speed of the bot
    /// </summary>
    public int speed;
    /// <summary>
    /// An integer that represents the radius of the explosion
    /// </summary>
    public int radius;
    /// <summary>
    /// An integer that stores the value of the bomb placed closer to the player
    /// </summary>
    public int proximity;
    /// <summary>
    /// An integer that stores the amount of times a bomb has hit the player
    /// </summary>
    public int hitScore;
    /// <summary>
    /// An array of ten that stores integers that range from zero to the amount of actions the bot can take, to be used as a probability array
    /// </summary>
    public int[] actionProbability;
    /// <summary>
    /// A flag that indicates if the bot is performing an action
    /// </summary>
    /*private bool _isActive = false;*/
    /// <summary>
    /// A vector that holds integers that represent the logical position of the bot in the map matrix
    /// </summary>
    public Vector2Int _botMatrixPos;
    private List<Vector3> pathVectorList = new List<Vector3>();
    /*private int currentNode;*/
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        actionProbability = new int[10];
    }
    
    /// <summary>
    /// A function that returns the matrix position of the bomb placed on the screen
    /// </summary>
    public void GetBotPosition()
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(_enemyBody.position.x / 10f);
        matrixPosition.y = Mathf.FloorToInt(_enemyBody.position.y / 10f);
        _botMatrixPos = matrixPosition;

    }

    /// <summary>
    /// Moves the bot on the screen given a path of logical map nodes
    /// </summary>
    /// <param name="path">A list of nodes to take as a path</param>
    public void MoveBot(List<Node> path)
    {
        
        Vector3 target = new Vector3(path[1].GetX(),path[1].GetY(),0f) * 10f + new Vector3(10f, 10f,0f) * 0.5f;
        /*Debug.Log("TARGET: "+target.x+", "+target.y);*/
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

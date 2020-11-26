using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class CharacterManager : MonoBehaviour
{
    public int health = 5;
    public float speed = 3f;
    private Rigidbody2D _playerBody;
    public PlayerActions playerInput;
    [SerializeField] private GameObject bomb;
    private Vector2 _lookDirection = new Vector2(1,0);
    private bool _isInvincible = false;
    private float _invincibleTime = 2f;
    private float _invincibleTimer;
    public Vector2Int playerMatrixPos;

    private void Awake()
    {
        playerInput = new PlayerActions();
    }

    private void OnEnable()
    {
        playerInput.Player_1.Enable();
        playerInput.Player_1.Move.performed += MoveOnPerformed;
        playerInput.Player_1.PlaceBomb.performed += PlaceBombOnPerformed;
        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
    }
    
    private void MoveOnPerformed(InputAction.CallbackContext context)
    {
        var move = context.ReadValue<Vector2>(); 
        var horizontal = move.x;
        var vertical = move.y;
        
        _lookDirection.Set(horizontal,vertical);

        if (_playerBody != null)
        {
            _playerBody.velocity = new Vector2(horizontal*speed*Time.fixedDeltaTime*100f,vertical*speed*Time.fixedDeltaTime*100f);
        }
        
        
    }

    private void PlaceBombOnPerformed(InputAction.CallbackContext context)
    {
        if (_playerBody != null)
        {
            
            Vector2 bombPosition = new Vector2();
            var position = _playerBody.position;

            if (_lookDirection.x < 0f)
            {
                bombPosition.Set(position.x-5f,position.y);
                GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
                GetBombPosition(_playerBody.position);
            
            }

            if (_lookDirection.x > 0f)
            {
                bombPosition.Set(position.x+5f,position.y);
                GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
                GetBombPosition(_playerBody.position);
            
            }

            if (_lookDirection.y < 0f)
            {
                bombPosition.Set(position.x,position.y-5f);
                GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
                GetBombPosition(_playerBody.position);
            
            }
        
            if (_lookDirection.y > 0f)
            {
                bombPosition.Set(position.x,position.y+5f);
                GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
                GetBombPosition(_playerBody.position);
            
            }
            
            
            
        }
        

    }
    
    private void GetBombPosition(Vector2 worldPosition)
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(worldPosition.x / 10f);
        matrixPosition.y = Mathf.FloorToInt(worldPosition.y / 10f);
        Debug.Log("BOMB PLACED ON: "+matrixPosition.x+", "+matrixPosition.y);
    }

    public void GetPlayerPosition()
    {
        Vector2Int matrixPosition = new Vector2Int();
        matrixPosition.x = Mathf.FloorToInt(_playerBody.position.x / 10f);
        matrixPosition.y = Mathf.FloorToInt(_playerBody.position.y / 10f);
        playerMatrixPos = matrixPosition;

    }

    void OnCollisionEnter2D(Collision2D other){

        
        if(other.gameObject.CompareTag("Destructible")){

            Debug.Log("COLLISION WITH DESTRUCTIBLE BLOCK");
        }

        if(other.gameObject.CompareTag("Indestructible")){

            Debug.Log("COLLISION WITH INDESTRUCTIBLE BLOCK");
            
        }


    }

    public void ReduceHealth()
    {
        if (_isInvincible)
        {
            return;
        }
        
        health -= 1;
        _isInvincible = true;
        _invincibleTimer = _invincibleTime;
        Debug.Log(health);
    }

    private void InvincibleTimeManager()
    {
        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
            {
                _isInvincible = false;
            }
        }
    }

    private void CheckDeath()
    {
        if (health == 0) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();
        InvincibleTimeManager();
        CheckDeath();
    }
}

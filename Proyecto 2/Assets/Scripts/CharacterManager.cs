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
    public int health;
    public float speed = 3f;
    private Rigidbody2D _playerBody;
    public PlayerActions playerInput;
    [SerializeField] private GameObject bomb;
    private Vector2 _lookDirection = new Vector2(1,0);

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
        
        
        _playerBody.velocity = new Vector2(horizontal*speed*Time.fixedDeltaTime*100f,vertical*speed*Time.fixedDeltaTime*100f);
        
    }

    private void PlaceBombOnPerformed(InputAction.CallbackContext context)
    {
        Vector2 bombPosition = new Vector2();
        var position = _playerBody.position;

        if (_lookDirection.x < 0f)
        {
            bombPosition.Set(position.x-0.03f,position.y);
            GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
        }

        if (_lookDirection.x > 0f)
        {
            bombPosition.Set(position.x+0.03f,position.y);
            GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
        }

        if (_lookDirection.y < 0f)
        {
            bombPosition.Set(position.x,position.y-0.8f);
            GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
        }
        
        if (_lookDirection.y > 0f)
        {
            bombPosition.Set(position.x,position.y+0.8f);
            GameObject bombObject = Instantiate(bomb,bombPosition, Quaternion.identity);
        }

    }

    void OnCollisionEnter2D(Collision2D other){

        
        if(other.gameObject.CompareTag("Destructible")){

            Debug.Log("COLLISION WITH DESTRUCTIBLE BLOCK");
        }

        if(other.gameObject.CompareTag("Indestructible")){

            Debug.Log("COLLISION WITH INDESTRUCTIBLE BLOCK");
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

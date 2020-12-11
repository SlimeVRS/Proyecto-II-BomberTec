using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Class that controls the behaviour of the bomb objects used by the player and the bots
/// </summary>
public class BombBehaviour : MonoBehaviour
{
    /// <summary>
    /// A float that represent the timer of the bomb
    /// </summary>
    public float timer = 3f;
    /// <summary>
    /// A float that represents the radius of explosion
    /// </summary>
    public float radius = 6f;
    private BoxCollider2D _horizontal;
    private BoxCollider2D _vertical;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        BoxCollider2D[] list = GetComponents<BoxCollider2D>();
        _vertical = list[0];
        _horizontal = list[1];
    }

    /// <summary>
    /// Changes the explosion radius after creation of the object
    /// </summary>
    public void ChangeExplosionRadius()
    {
        _vertical.size = new Vector2(1,radius);
        _horizontal.size = new Vector2(radius,1);
    }


    /// <summary>
    /// Function that manages the explosion action of the bomb
    /// </summary>
    public void Explode()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Debug.Log("BOOOOM");
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Destructible") && timer<=0.05)
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player")&&timer<=0.05)
        {
            other.GetComponent<CharacterManager>().ReduceHealth();
        }
    }


    // Update is called once per frame
    void Update()
    {
        Explode();
    }
}

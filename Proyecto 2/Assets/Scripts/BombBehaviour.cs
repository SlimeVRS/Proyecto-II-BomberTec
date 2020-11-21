using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public float timer = 3f;
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
        
        _vertical.size = new Vector2(2.5f,14);
        _horizontal.size = new Vector2(radius,2.5f);
    }

    public void ChangeExplosionRadius()
    {
        _vertical.size = new Vector2(1,radius);
        _horizontal.size = new Vector2(radius,1);
    }


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

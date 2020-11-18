using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public float timer = 15f;
    public float radius = 6f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Explode()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("BOOOOM");
            Destroy(gameObject);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        Explode();
    }
}

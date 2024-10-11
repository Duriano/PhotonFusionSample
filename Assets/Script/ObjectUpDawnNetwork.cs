using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ObjectUpDawnNetwork : NetworkBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 1.0f;
    public NetworkTransform networkTransform;

    private Vector3 startPosition;


    void Start()
    {
        networkTransform = GetComponent<NetworkTransform>();

        startPosition = transform.position;
    }

    public override void Spawned()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Object.IsSpawnable)
        {
            if(Runner.IsClient)
            {
                // Shared Mode
                if(Runner.IsSharedModeMasterClient)
                    Movement();
            }
            else
            {
                // Server and Host Mode.
                Movement();
            }
                
        }
    }

    void Movement()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}


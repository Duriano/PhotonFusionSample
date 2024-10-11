using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject networkObject;

    private NetworkRunner runner;

    private void Awake() {
        runner = App.LocalRunner;
    }

    void Start()
    {
        if(runner.GameMode == GameMode.Shared)
        {
            if(runner.IsSharedModeMasterClient)
            {
                runner.Spawn(networkObject, transform.position);
            }
        }
        else if(!runner.IsClient)
        {
            runner.Spawn(networkObject, transform.position);
        }
    }
}

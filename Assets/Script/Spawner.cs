using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    private NetworkRunner runner;
    private NetworkEvents networkEvents;

    void Awake()
    {
        runner = App.LocalRunner;
        networkEvents = GetComponent<NetworkEvents>();
        runner.AddCallbacks(networkEvents);
        networkEvents.PlayerJoined.AddListener(OnPlayerJoined);
    }

    void Start()
    {
        if (runner.GameMode == GameMode.Shared)
        {
            // Shared Mode.
            Spawn(runner.LocalPlayer);
        }
        else if (!runner.IsClient)
        {
            // Server or Host
            foreach (var item in runner.ActivePlayers)
            {
                Spawn(item);
            }
        }
    }

    private void OnDestroy()
    {
        networkEvents.PlayerJoined.RemoveAllListeners();
        runner.RemoveCallbacks(networkEvents);
    }

    // Server or Host
    void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (!runner.IsClient && runner.GameMode != GameMode.Shared)
        {
            Spawn(player);
        }
    }


    void Spawn(PlayerRef playerRef)
    {
        NetworkObject obj = runner.Spawn(prefab, new Vector3(0, 1, 0), Quaternion.identity, playerRef);
    }
}

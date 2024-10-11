using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerData : NetworkBehaviour
{
    public static PlayerData Local;
    [Networked]
    public NetworkString<_16> PlayerName { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public override void Spawned()
    {
        if(Runner.LocalPlayer == Object.InputAuthority)
            Local = this;

        if (Runner.GameMode == GameMode.Shared)
        { 
            // Shard Mode Only.
            if (Object.HasInputAuthority)
                Runner.SetPlayerObject(Object.InputAuthority, Object);
        }
        else if (!Runner.IsClient)
        {
            // Server And Host Mode
            Runner.SetPlayerObject(Object.InputAuthority, Object);
        }
    }

    [Rpc]
    public void RPC_SetName(NetworkString<_16> nickName)
    {
        gameObject.name = nickName.ToString();
        PlayerName = nickName;
    }

}

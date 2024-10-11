using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class InputController : NetworkBehaviour
{
    [Networked]
    private NetworkButtons _prevData { get; set; }
    public NetworkButtons PrevButtons { get => _prevData; set => _prevData = value; }

    public override void Spawned()
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        InputData currentInput = new InputData();
        
        currentInput.Buttons.Set(InputButton.LEFT, Input.GetKey(KeyCode.A));
        currentInput.Buttons.Set(InputButton.RIGHT, Input.GetKey(KeyCode.D));

        input.Set(currentInput);
    }
}

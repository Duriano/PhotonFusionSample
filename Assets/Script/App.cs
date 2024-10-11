using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour, INetworkRunnerCallbacks
{
    public static NetworkRunner LocalRunner;
    public static App Instance;
    [SerializeField] private string roomName = "";
    [SerializeField] private string nickName = "";
    [SerializeField] private GameObject objPlayerSpawn;
    [SerializeField] private GameMode gameMode;

    const string LOBBY_PUBLIC = "LOBBY_PUBLIC";

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void CreateRunner()
    {
        if (!LocalRunner)
        {
            LocalRunner = new GameObject("Runner").AddComponent<NetworkRunner>();
            LocalRunner.AddCallbacks(this);
        }
    }

    [Button]
    public async void EnterLobby()
    {
        CreateRunner();
        await LocalRunner.JoinSessionLobby(SessionLobby.Custom, LOBBY_PUBLIC);
    }

    [Button]
    public async void StartGame()
    {
        CreateRunner();
        StartGameResult result = await LocalRunner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = roomName,
            CustomLobbyName = LOBBY_PUBLIC

        });

        if (result.Ok)
        {
            SceneManager.LoadScene("GamePlay");
        }
    }

    [Button("Set you nickname.")]
    public void SetNickName()
    {
        PlayerData.Local.RPC_SetName(nickName);
    }


    #region Fusion Callbacks    

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
        if (!LocalRunner.IsClient)
        {
            runner.Spawn(objPlayerSpawn, new Vector3(0, 1, 0), Quaternion.identity, player);
        }
        else if(LocalRunner.GameMode == GameMode.Shared)
        {
            if (player == LocalRunner.LocalPlayer)
            {
                LocalRunner.Spawn(objPlayerSpawn, new Vector3(0, 1, 0), Quaternion.identity, player);
            }
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        foreach (var item in sessionList)
        {
            Debug.Log(item.Name);
        }

    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {

    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
    #endregion
}

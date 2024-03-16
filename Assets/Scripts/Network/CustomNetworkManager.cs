using Mirror;
using System;
using UnityEngine;

namespace OnlineFPS.Network
{
    public class CustomNetworkManager : NetworkManager
    {
        public static Action OnClientConnected;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log("on server add player");
            base.OnServerAddPlayer(conn);
            OnClientConnected?.Invoke();
        }
    }
}
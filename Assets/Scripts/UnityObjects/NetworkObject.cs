using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObject : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        CheckForPackets();
    }

    public void ConnectToServer()
    {
        ServiceProvider.NetworkManager.ConnectToServer();

    }

    public void CheckForPackets()
    {
        if (ServiceProvider.NetworkManager.IsConnected())
        {
            ServiceProvider.NetworkManager.ReceivePacket();
        }
    }

    public void SendTestPacket()
    {
        var poco = new TestPOCO()
        {
            Line = "test"
        };

        Packet packet = new Packet()
        {
            PlayerId = ServiceProvider.PlayerManager.GetHomePlayer().PlayerId,
            CommandId = (short)CommandId.test
        };

        ServiceProvider.NetworkManager.AddPocoToPacket(packet, poco);
        ServiceProvider.NetworkManager.SendPacket(packet);
    }
}

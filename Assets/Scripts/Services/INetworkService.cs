using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public interface INetworkService 
{
    NetworkStream GetNetworkStream();
    void CleanNetworkResources();
    Task SendPacket(Packet packet);
    Task ReceivePacket();
    void AddPocoToPacket(Packet packet, POCO poco);
    POCO GetPocoFromPacket(Packet packet);
    Task ConnectToServer();
    bool IsConnected();
}

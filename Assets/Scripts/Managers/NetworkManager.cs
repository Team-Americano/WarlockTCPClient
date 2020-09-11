using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

public class NetworkManager : INetworkService
{
    private readonly int _port = 28852;
    private readonly string _hostname = "13.91.38.28";  //"localhost";// 
    private readonly int _bufferSize = 1024 * 8;
    public List<Packet> Packets { get; set; }
    public static TcpClient TcpClient { get; private set; }


    public Task ConnectToServer()
    {
        TcpClient = new TcpClient(_hostname, _port);
        return Task.FromResult(0);
    }

    public NetworkStream GetNetworkStream()
    {
        return TcpClient.GetStream();
    }

    public async Task ReceivePacket()
    {
            byte[] buffer = new byte[_bufferSize];
            int bytes = await GetNetworkStream().ReadAsync(buffer, 0, buffer.Length);

            string packetStr = Encoding.UTF8.GetString(buffer, 0, bytes);
            Packet packet = JsonUtility.FromJson<Packet>(packetStr);
            System.Console.WriteLine("I am nothing");

        if (packet != null)
        {
            ServiceProvider.InputManager.HandleCommand(packet);
        }
    }

    public Task SendPacket(Packet packet)
    {
        string packetStr = JsonUtility.ToJson(packet);
        byte[] buffer = Encoding.UTF8.GetBytes(packetStr);
        GetNetworkStream().Write(buffer, 0, buffer.Length);
        return Task.FromResult(0);
    }

    public void AddPocoToPacket(Packet packet, POCO poco)
    {
        string pocoStr = JsonUtility.ToJson(poco);
        packet.POCOJson = pocoStr;
    }

    public POCO GetPocoFromPacket(Packet packet)
    {
        var pocoJson = packet.POCOJson;
        var poco = JsonUtility.FromJson<POCO>(packet.POCOJson);
        System.Console.WriteLine("blank");
        return poco;
    }

    public bool IsConnected()
    {
        return TcpClient.Connected;
    }

    public void CleanNetworkResources()
    {
        GetNetworkStream().Close();
        TcpClient.Close();
    }
}

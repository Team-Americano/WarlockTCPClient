using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceProvider
{
    public static IInputService InputManager { get; } = new InputManager();
    public static INetworkService NetworkManager { get; } = new NetworkManager();
    public static IPlayerIdService PlayerManager { get; } = new PlayerManager();
}

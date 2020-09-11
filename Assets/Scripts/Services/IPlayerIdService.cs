using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerIdService
{
    void AddPlayer(string playerId);
    void RemovePlayer(string playerId);
    Player GetPlayer(string playerId);
    Player GetHomePlayer();
    Player GetOpponentPlayer();
    void SetHomePlayer(string playerId);
    void SetOpponentPlayer(string playerId);
}

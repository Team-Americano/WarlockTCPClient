using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IPlayerIdService
{
    private Dictionary<string, Player> _players;
    private Player _homePlayer; // Need to set this somewhere
    private Player _opponentPlayer; // Need to set this somewhere

    public PlayerManager ()
    {
        _players = new Dictionary<string, Player>();
    }

    public void AddPlayer(string playerId)
    {
        var newPlayer = new Player()
        {
            PlayerId = playerId
        };

        _players.Add(playerId, newPlayer);
    }

    public void RemovePlayer(string playerId)
    {
        _players.Remove(playerId);
    }

    public Player GetPlayer(string playerId)
    {
        return _players[playerId];
    }

    public Player GetHomePlayer()
    {
        return _homePlayer;
    }

    public Player GetOpponentPlayer()
    {
        return _opponentPlayer;
    }

    public void SetHomePlayer(string playerId)
    {
        AddPlayer(playerId);
        _homePlayer = _players[playerId];
    }

    public void SetOpponentPlayer(string playerId)
    {
        AddPlayer(playerId);
        _opponentPlayer = _players[playerId];
    }
}

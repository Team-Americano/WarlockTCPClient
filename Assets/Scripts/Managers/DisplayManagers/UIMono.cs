using Assets.Scripts.UnityObjects.UiObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMono : MonoBehaviour
{
    [SerializeField]
    private GameUiObject _gameUiObject;

    // Start is called before the first frame update
    void Start()
    {
        ServiceProvider.InputManager.UpdateUiEvent.AddListener(UpdateUi);
        _gameUiObject.Round.text = "Round: 1";
        _gameUiObject.Mana.text = "Mana: 0";
        _gameUiObject.Score.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUi()
    {
        _gameUiObject.Round.text = "Round: " + ServiceProvider.InputManager.Game.Round.ToString();
        _gameUiObject.Mana.text = "Mana: " + ServiceProvider.PlayerManager.GetHomePlayer().Mana.ToString();
        _gameUiObject.Score.text = "Score: " + ServiceProvider.PlayerManager.GetHomePlayer().Score.ToString();

        var output = ("Round {0}, Mana {1}, Score {2}", _gameUiObject.Round.text, _gameUiObject.Mana.text, _gameUiObject.Score.text);
        Debug.Log(output);
    }
    public void EndTurn()
    {
        Debug.Log("End Turn");

        ServiceProvider.InputManager.DraftActors();
    }
}

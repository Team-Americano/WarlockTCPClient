﻿using Assets.Scripts.GameLogic.ActorComponents;
using Assets.Scripts.Managers.UserActions;
using Assets.Scripts.UnityObjects.UiObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PartyMono : MonoBehaviour
{
    [SerializeField]
    private ActorSlot[] _actorSlots;

    [SerializeField]
    private SpriteRenderer[] _homePlayerSprites;

    [SerializeField]
    private SpriteRenderer[] _enemyPlayerSprites;

    [SerializeField]
    private CardUiObject[] _enemyCardUi;

    private Dictionary<string, Sprite> _spriteDirectory;

    public void Start()
    {
        _spriteDirectory = new Dictionary<string, Sprite>()
        {
            // Undead
            { "Zombie", Resources.Load<Sprite>("Monsters/Undead/Zombie") },
            { "Skeleton", Resources.Load<Sprite>("Monsters/Undead/Skeleton") },
            { "Ghost", Resources.Load<Sprite>("Monsters/Undead/Ghost") },
            { "Wraith", Resources.Load<Sprite>("Monsters/Undead/Wraith") },
            { "Revenant", Resources.Load<Sprite>("Monsters/Undead/Revenant") },
            { "Lich", Resources.Load<Sprite>("Monsters/Undead/Lich") },
            // Glacial
            { "Snowfriend", Resources.Load<Sprite>("Monsters/Glacial/Snowfriend") },
            { "Ice Wasp", Resources.Load<Sprite>("Monsters/Glacial/Ice Wasp") },
            { "Frost Sprite", Resources.Load<Sprite>("Monsters/Glacial/Frost Sprite") },
            { "Ice Pike", Resources.Load<Sprite>("Monsters/Glacial/Ice Pike") },
            { "Glacial Guard", Resources.Load<Sprite>("Monsters/Glacial/Glacial Guard") },
            { "Ice Drake", Resources.Load<Sprite>("Monsters/Glacial/Ice Drake") },
            // Infernal
            { "Lemure", Resources.Load<Sprite>("Monsters/Infernal/Lemure") },
            { "Imp", Resources.Load<Sprite>("Monsters/Infernal/Imp") },
            { "Inferno", Resources.Load<Sprite>("Monsters/Infernal/Inferno") },
            { "Incubus", Resources.Load<Sprite>("Monsters/Infernal/Incubus") },
            { "Gobbler", Resources.Load<Sprite>("Monsters/Infernal/Gobbler") },
            { "Pit Lord", Resources.Load<Sprite>("Monsters/Infernal/Pit Lord") },
            // Bestial
            { "Dire Wolf", Resources.Load<Sprite>("Monsters/Bestial/Dire Wolf") },
            { "Bully Toad", Resources.Load<Sprite>("Monsters/Bestial/Bully Toad") },
            { "Black Asp", Resources.Load<Sprite>("Monsters/Bestial/Black Asp") },
            { "Spider Queen", Resources.Load<Sprite>("Monsters/Bestial/Spider Queen") },
            { "Arch Druid", Resources.Load<Sprite>("Monsters/Bestial/Arch Druid") },
            { "Vine Behemoth", Resources.Load<Sprite>("Monsters/Bestial/Vine Behemoth") },
            // Aberrant
            { "Slime", Resources.Load<Sprite>("Monsters/Aberrant/Slime") },
            { "Gazer", Resources.Load<Sprite>("Monsters/Aberrant/Gazer") },
            { "Morlock", Resources.Load<Sprite>("Monsters/Aberrant/Morlock") },
            { "Deep Hound", Resources.Load<Sprite>("Monsters/Aberrant/Deep Hound") },
            { "Mind Eater", Resources.Load<Sprite>("Monsters/Aberrant/Mind Eater") },
            { "Deep Tyrant", Resources.Load<Sprite>("Monsters/Aberrant/Deep Tyrant") },
            // Aquatic
            { "Mer Guard", Resources.Load<Sprite>("Monsters/Aquatic/Mer Guard") },
            { "Sea Worm", Resources.Load<Sprite>("Monsters/Aquatic/Sea Worm") },
            { "Man-O-War", Resources.Load<Sprite>("Monsters/Aquatic/Man-O-War") },
            { "Chomper", Resources.Load<Sprite>("Monsters/Aquatic/Chomper") },
            { "Kraken", Resources.Load<Sprite>("Monsters/Aquatic/Kraken") },
            { "Trident King", Resources.Load<Sprite>("Monsters/Aquatic/Trident King") },
        };

        for (int i = 0; i < _actorSlots.Length; i++)
        {
            _actorSlots[i].SlotNumber = i;
            _actorSlots[i].PartyMono = this;
        }

        ServiceProvider.InputManager.DraftEvent.AddListener(UpdateParty);
        ServiceProvider.InputManager.UpdateUiEvent.AddListener(UpdateEnemyParty);
    }

    public void PlaceCard(CardDragDrop card, GameObject actorSlot)
    {
        card.OriginPos = actorSlot.GetComponent<RectTransform>().anchoredPosition;
        card.SetCardToOriginalPosition();
        var homePlayer = ServiceProvider.PlayerManager.GetHomePlayer();
        var slotNumber = actorSlot.GetComponent<ActorSlot>().SlotNumber;

        // Add the card to the Party.
        homePlayer.TempParty[slotNumber] = card.Actor;

        // Remove that card from the hand
        var cardToRemoveFromHand = homePlayer.Hand.Where(x => x.CardId == card.Actor.CardId).FirstOrDefault();
        if (cardToRemoveFromHand != null)
        {
            homePlayer.Hand.Remove(cardToRemoveFromHand);
            ServiceProvider.PlayerManager.GetHomePlayer().Mana -= cardToRemoveFromHand.ManaCost.CurrentValue;
        }
    }

    public void UpdateParty()
    {
        var player = ServiceProvider.PlayerManager.GetHomePlayer();
        player.Party = new List<Actor>();

        Debug.Log("Your Party Size: " + player.Party.Count());
        if (player.TempParty == null) return;

        foreach (var actor in player.TempParty)
        {
            if(actor != null)
            {
                player.Party.Add(actor);
            }
        }

        for (int i = 0; i < _homePlayerSprites.Length; i++)
        {
            if (i < player.Party.Count())
            {
                _homePlayerSprites[i].sprite = _spriteDirectory[player.Party[i].Name];
                _homePlayerSprites[i].GetComponent<ActorId>().Id = player.Party[i].CardId;
            }
            else
            {
                Debug.Log("That was not found");
                _homePlayerSprites[i].sprite = null;
                _homePlayerSprites[i].GetComponent<ActorId>().Id = -1;
            }
        }

        //UpdateEnemyParty();
    }

    public void UpdateEnemyParty()
    {
        var player = ServiceProvider.PlayerManager.GetOpponentPlayer();

        if (player.Party == null) return;

        for (int i = 0; i < _enemyPlayerSprites.Length; i++)
        {
            if (i < player.Party.Count())
            {
                _enemyPlayerSprites[i].gameObject.SetActive(true);
                _enemyPlayerSprites[i].sprite = _spriteDirectory[player.Party[i].Name];
                _enemyPlayerSprites[i].GetComponent<ActorId>().Id = player.Party[i].CardId;
            }
            else
            {
                Debug.Log("That was not found");
                _enemyPlayerSprites[i].gameObject.SetActive(false);
                _enemyPlayerSprites[i].GetComponent<ActorId>().Id = -1;
            }
        }

        for (int i = 0; i < _enemyCardUi.Length; i++)
        {
            if (i < player.Party.Count())
            {
                FillCardUI(_enemyCardUi[i].gameObject, (short)i);
            }
            else
            {
                Debug.Log("That was not found");
                _enemyCardUi[i].gameObject.SetActive(false);
            }
        }
    }

    private void FillCardUI(GameObject cardUI, short cardIndex)
    {
        cardUI.SetActive(true);

        var cardUiObject = cardUI.GetComponent<CardUiObject>();
        var thisCard = ServiceProvider.PlayerManager.GetOpponentPlayer().Party[cardIndex];

        Color backgroundColor = default;
        Color forgroundColor = default;
        if (thisCard.Rarity == "Rare")
        {
            backgroundColor = Color.red;
            forgroundColor = Color.white;
        }
        else if (thisCard.Rarity == "Uncommon")
        {
            backgroundColor = Color.blue;
            forgroundColor = Color.white;
        }
        else if (thisCard.Rarity == "Common")
        {
            backgroundColor = Color.green;
            forgroundColor = Color.black;
        }

        cardUiObject.ManaText.text = "Mana: " + thisCard.ManaCost.BaseValue.ToString();
        cardUiObject.ManaText.color = forgroundColor;

        cardUiObject.NameText.text = thisCard.Name;
        cardUiObject.NameText.color = forgroundColor;

        cardUiObject.HealthText.text = "Health: " + thisCard.Health.BaseValue.ToString();
        cardUiObject.HealthText.color = forgroundColor;

        cardUiObject.DefenseText.text = "Defense: " + thisCard.Defense.BaseValue.ToString();
        cardUiObject.DefenseText.color = forgroundColor;

        cardUiObject.AttackText.text = "Attack: " + thisCard.Attack.BaseValue.ToString();
        cardUiObject.AttackText.color = forgroundColor;

        cardUiObject.SpeedText.text = "Speed: " + thisCard.Speed.BaseValue.ToString();
        cardUiObject.SpeedText.color = forgroundColor;

        cardUiObject.PrecisionText.text = "Agility: " + thisCard.Precision.BaseValue.ToString(); //Agility == Precision
        cardUiObject.PrecisionText.color = forgroundColor;

        cardUiObject.Background.color = backgroundColor;
    }
}

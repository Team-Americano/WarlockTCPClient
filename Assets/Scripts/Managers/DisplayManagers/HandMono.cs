using Assets.Scripts.Managers.UserActions;
using Assets.Scripts.UnityObjects.UiObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMono : MonoBehaviour
{
    //[SerializeField]
    public GameObject[] _cardUiObjects;

    // Start is called before the first frame update
    void Start()
    {
        ServiceProvider.InputManager.DrawEvent.AddListener(UpdateHand);
    }

    public void UpdateHand()
    {
        for (int i = 0; i < ServiceProvider.PlayerManager.GetHomePlayer().Hand.Count; i++)
        {
            if (_cardUiObjects[i] != null)
            {
                _cardUiObjects[i].SetActive(true);

                var cardDragDrop = _cardUiObjects[i].GetComponent<CardDragDrop>();
                var rectTransform = _cardUiObjects[i].GetComponent<RectTransform>();
                var cardUiObject = _cardUiObjects[i].GetComponent<CardUiObject>();

                rectTransform.anchoredPosition = cardDragDrop.PositionOnStart;

                cardDragDrop.Actor = ServiceProvider.PlayerManager.GetHomePlayer().Hand[i]; 
                var thisCard = cardDragDrop.Actor;

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
                
                Debug.Log(cardUiObject.NameText.text);
            }
            else
            {
                _cardUiObjects[i].gameObject.SetActive(false);
            }
        }

    }
}

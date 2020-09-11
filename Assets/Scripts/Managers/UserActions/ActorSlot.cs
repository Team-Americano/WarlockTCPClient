using Assets.Scripts.Managers.UserActions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActorSlot : MonoBehaviour, IDropHandler
{

    public int SlotNumber;
    public PartyMono PartyMono;

    private CardDragDrop slottedCard;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            var card = eventData.pointerDrag.GetComponent<CardDragDrop>();

            if(slottedCard == null)
            {
                slottedCard = card;
            }
            else
            {
                var cardToSwap = slottedCard;

                if (card.CurrentSlot != null) cardToSwap.CurrentSlot = card.CurrentSlot;
                // if card comes from hand, then set active to false --> temporary until hand slots are implemented
                else cardToSwap.gameObject.SetActive(false);

                card.CurrentSlot = this;

                if(cardToSwap.gameObject.activeSelf) PartyMono.PlaceCard(cardToSwap, cardToSwap.CurrentSlot.gameObject);
            }
            
            PartyMono.PlaceCard(card, gameObject);
        }
    }
}


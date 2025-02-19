using System.Collections.Generic;
using UnityEngine;

public class MakeAllHandTrump : Consumable
{
    public override void Activate()
    {
        List<GameObject> cardsInHand = ServiceLocator.Current.Get<CardGameController>().cardsInHand;
        foreach (GameObject card in cardsInHand)
        {
            card.GetComponent<Card>().cardData.cardInfo.suit = CardInfo.CardSuit.Trump;
            card.GetComponent<CardView>().SynchronizeNewView();
        }
        
        Destroy(this); 
    }
}
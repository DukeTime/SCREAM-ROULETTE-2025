using System.Collections.Generic;
using UnityEngine;

public class MakeAllHand4 : Consumable
{
    public override void Activate()
    {
        List<GameObject> cardsInHand = ServiceLocator.Current.Get<CardGameController>().cardsInHand;
        foreach (GameObject card in cardsInHand)
        {
            card.GetComponent<Card>().cardData.cardInfo.value = 4;
            card.GetComponent<CardView>().SynchronizeNewView();
        }
        
        Destroy(this); 
    }
}
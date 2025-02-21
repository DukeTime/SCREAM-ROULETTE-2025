using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyOpenCard : Consumable
{
    public override void Activate()
    {
        List<GameObject> enemyCards = ServiceLocator.Current.Get<EnemyCardsController>().cardsObjects;
        foreach (GameObject cardObj in enemyCards)
        {
            EnemyCard card = cardObj.GetComponent<EnemyCard>();
            if (card.state == EnemyCard.EnemyCardState.Opened)
            {
                card.Beat();
                break;
            }
        }
        
        Destroy(this); 
    }
}
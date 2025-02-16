using System;
using UnityEngine;

public class EnemyCard : MonoBehaviour
{
    public CardInfo cardInfo = new CardInfo();
    
    public enum EnemyCardState
    {
        Closed,
        Opened,
        Beated
    };
    public EnemyCardState state = EnemyCardState.Closed;
    
    void Start()
    {
    }

    void Update()
    {
        
    }

    public bool TryPlayCard(CardInfo enemyCardInfo)
    {
        if (enemyCardInfo.suit == cardInfo.suit || enemyCardInfo.suit == CardInfo.CardSuit.Trump)
        {
            PlayCard(enemyCardInfo);
            return true;
        }
        return false;
    }

    public void PlayCard(CardInfo cardInfo)
    {
        
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCard : MonoBehaviour
{
    public CardInfo cardInfo;
    
    
    public enum EnemyCardState
    {
        Closed,
        Opened,
        Beated
    };
    public EnemyCardState state = EnemyCardState.Closed;
    
    void Start()
    {
        cardInfo = GetComponent<CardInfo>();
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
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    public CardInfo cardInfo;
    
    private MovebleSmoothDump moveble;
    
    public enum CardState
    {
        Hand,
        Played
    };
    public CardState state = CardState.Hand;
    
    void Start()
    {
        moveble = GetComponent<MovebleSmoothDump>();
        cardInfo = GetComponent<CardInfo>();
    }

    void Update()
    {
        
    }

    public bool TryPlayCard(GameObject slot)
    {
        EnemyCard enemyCard = slot.GetComponent<EnemyCard>();
        if (enemyCard != null)
        {
            if (enemyCard.TryPlayCard(gameObject))
            {
                PlayCard();
                state = CardState.Played;
                return true;
            }
        }
        return false;
    }

    private void PlayCard()
    {
        ServiceLocator.Current.Get<HandController>().cardsInHand.Remove(gameObject);
    }

}
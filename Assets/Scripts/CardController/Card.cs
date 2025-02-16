using System;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardInfo cardInfo = new CardInfo();
    
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
    }

    void Update()
    {
        
    }

    public bool TryPlayCard(GameObject slot)
    {
        EnemyCard enemyCard = slot.GetComponent<EnemyCard>();
        if (enemyCard != null)
        {
            if (enemyCard.TryPlayCard(cardInfo))
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
        
    }

}
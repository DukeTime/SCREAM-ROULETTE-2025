using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCard : MonoBehaviour
{
    public CardInfo cardInfo;
    public List<GameObject> playerCardsOn = new List<GameObject>();
    
    public enum EnemyCardState
    {
        Closed,
        Opened,
        Beated
    };
    public EnemyCardState state = EnemyCardState.Closed;
    public int Bonus => (int)(cardInfo.value == 1 ? 1 : cardInfo.value * 0.5f);
    public bool isOnTheBoard;

    public Action OnOpened;
    public Action OnPlayCard;
    public Action OnBeated;
    public Action OnGetBonus;
    public Action OnAppearedOnTheBoard;

    void Start()
    {
        cardInfo = GetComponent<CardInfo>();
    }

    void Update()
    {
        
    }

    public void AppearOnTheBoard()
    {
        isOnTheBoard = true;
        OnAppearedOnTheBoard?.Invoke();
    }

    public IEnumerator Open()
    {
        while (!isOnTheBoard)
            yield return null;
        
        state = EnemyCardState.Opened;
        OnOpened?.Invoke();
    }

    public bool TryPlayCard(GameObject playerCard)
    {
        if (state == EnemyCardState.Opened)
        {
            CardInfo playerCardInfo = playerCard.GetComponent<CardInfo>();

            if (playerCardInfo.suit == cardInfo.suit || playerCardInfo.suit == CardInfo.CardSuit.Trump)
            {
                StartCoroutine(PlayCard(playerCard));
                return true;
            }
        }
        return false;
    }

    public IEnumerator PlayCard(GameObject playerCard)
    {
        while (playerCard.GetComponent<MovebleSmoothDump>().isAnimating)
        {
            yield return null;
        }
        
        CardInfo playerCardInfo = playerCard.GetComponent<CardInfo>();
        playerCardsOn.Add(playerCard);
        
        if (playerCardInfo.value >= cardInfo.value)
            StartCoroutine(Beat());
        else
        {
            cardInfo.value -= playerCardInfo.value;
            OnPlayCard?.Invoke();
        }
    }

    public void GetBonus()
    {
        cardInfo.value += Bonus;
        
        OnGetBonus?.Invoke();
    }

    private IEnumerator Beat()
    {
        state = EnemyCardState.Beated;
        cardInfo.value = 0;

        yield return new WaitForSeconds(0.75f);
        
        OnBeated?.Invoke();
        Destroy(gameObject);
            
    }
}
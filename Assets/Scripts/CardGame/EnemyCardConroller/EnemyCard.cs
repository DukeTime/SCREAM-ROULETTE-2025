using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCard : MonoBehaviour
{
    public EnemyCardData cardData;
    public List<GameObject> playerCardsOn = new List<GameObject>();
    
    public enum EnemyCardState
    {
        Closed,
        Opened,
        Beated
    };
    public EnemyCardState state = EnemyCardState.Closed;
    
    public bool isOnTheBoard;

    public Action OnOpened;
    public Action OnPlayCard;
    public Action OnBeated;
    public Action OnGetBonus;
    public Action OnAppearedOnTheBoard;

    public void Init(EnemyCardData data)
    {
        cardData = data;
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
        cardData.bonus = (int)(cardData.cardInfo.value == 1 ? 1 : cardData.cardInfo.value * 0.5f);
        OnOpened?.Invoke();
    }

    public bool TryPlayCard(GameObject playerCard)
    {
        if (state == EnemyCardState.Opened)
        {
            CardInfo playerCardData = playerCard.GetComponent<Card>().cardData.cardInfo;

            if (playerCardData.suit == cardData.cardInfo.suit || playerCardData.suit == CardInfo.CardSuit.Trump)
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
        
        CardInfo playerCardData = playerCard.GetComponent<Card>().cardData.cardInfo;
        playerCardsOn.Add(playerCard);
        
        if (playerCardData.value >= cardData.cardInfo.value)
            StartCoroutine(Beat());
        else
        {
            cardData.cardInfo.value -= playerCardData.value;
            OnPlayCard?.Invoke();
        }
    }

    public void GetBonus()
    {
        cardData.cardInfo.value += cardData.bonus;
        
        OnGetBonus?.Invoke();
        
        cardData.bonus = (int)(cardData.cardInfo.value == 1 ? 1 : cardData.cardInfo.value * 0.5f);
    }

    private IEnumerator Beat()
    {
        state = EnemyCardState.Beated;
        cardData.cardInfo.value = 0;

        yield return new WaitForSeconds(0.75f);
        
        OnBeated?.Invoke();
        Destroy(gameObject);
            
    }
    
    public void Delete()
    {
        StopAllCoroutines();
        transform.DOKill();
        gameObject.SetActive(false);
    }
}
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCardsController : MonoBehaviour, IService
{
    public List<GameObject> cardsObjects;
    public List<EnemyCardData> cardsData;
    
    public Action OnAllCardsDefeated;
    public Action OnCardBeaten;

    [SerializeField] private EnemyData data;
    [SerializeField] private GameObject cardDefaultPrefab;
    
    private int _cardBeated;
    private CardGameController _cardGameController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _cardGameController.GameStart += GameStart;
        
        LoadDeckCards();
        SpawnCards();
        
        foreach (GameObject card in cardsObjects)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            
            enemyCard.OnBeated += CardBeaten;
        }
    }

    private void GameStart()
    {
        StartCoroutine(cardsObjects[0].GetComponent<EnemyCard>().Open());
    }

    public void Turn()
    {
        if (_cardGameController.state != CardGameController.GameState.GameEnd)
        {
            foreach (GameObject card in cardsObjects)
            {
                EnemyCard enemyCard = card.GetComponent<EnemyCard>();
                if (enemyCard.state == EnemyCard.EnemyCardState.Opened)
                    enemyCard.GetBonus();
            }
        }
    }

    private void CardBeaten()
    {
        if (_cardBeated == cardsData.Count - 1)
        {
            Destroy(cardsObjects[0]);
            OnAllCardsDefeated?.Invoke();
            return;
        }
        
        _cardBeated++;
        cardsObjects.RemoveAt(0);
        StartCoroutine(cardsObjects[0].GetComponent<EnemyCard>().Open());
        
        OnCardBeaten?.Invoke();
    }

    private void SpawnCards()
    {
        foreach (EnemyCardData card in cardsData)
        {
            GameObject newEnemyCard = Instantiate(cardDefaultPrefab, new Vector3(0, 7, 0),
                Quaternion.Euler(new Vector3(0, 180, 0)));
            newEnemyCard.GetComponent<EnemyCard>().Init(card);
            cardsObjects.Add(newEnemyCard);
        }
    }
    
    private void LoadDeckCards()
    {
        cardsData = data.deck;
    }
}
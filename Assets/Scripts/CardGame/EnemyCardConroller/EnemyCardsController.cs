using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCardsController : MonoBehaviour, IService
{
    public List<GameObject> cardsObjects;
    public List<GameObject> cardsPrefs;
    
    private int _cardBeated;
    private CardGameController _cardGameController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _cardGameController.GameStart += GameStart;
        
        SpawnCards();
        foreach (GameObject card in cardsObjects)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            
            enemyCard.OnBeated += CardBeated;
        }
    }

    private void GameStart()
    {
        StartCoroutine(cardsObjects[0].GetComponent<EnemyCard>().Open());
    }

    public void Turn()
    {
        foreach (GameObject card in cardsObjects)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            if (enemyCard.state == EnemyCard.EnemyCardState.Opened)
                enemyCard.GetBonus();
        }
    }

    private void CardBeated()
    {
        _cardBeated++;

        StartCoroutine(cardsObjects[_cardBeated].GetComponent<EnemyCard>().Open());
    }

    private void SpawnCards()
    {
        foreach (GameObject card in cardsPrefs)
        {
            cardsObjects.Add(Instantiate(card, new Vector3(0, 7, 0), Quaternion.Euler(new Vector3(0, 180, 0))));
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardGameController : MonoBehaviour, IService
{
    public List<GameObject> cardsInDeck;
    public int startCountOfCards = 5;
    
    public Action GameStart;
    public Action GameEnd;
    public Action MonologueEnd;
    
    private EnemyCardsController _enemyCardsController;
    private EnemyCardsView _enemyCardsView;
    
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }
    public GameState state;
    
    void Start()
    {
        _enemyCardsController = ServiceLocator.Current.Get<EnemyCardsController>();
        
        StartCoroutine(EnemyMonologue());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            DrawCard();
    }

    public void Victory()
    {
        
    }

    private IEnumerator EnemyMonologue()
    {
        MonologueEnd?.Invoke();
        
        yield return new WaitForSeconds(1.5f);
        
        StartGame();
    }

    private void StartGame()
    {
        GameStart?.Invoke();
        
        ShuffleDeck();
        for (int i = 0; i < startCountOfCards; i++)
            DrawCard();
    }

    public void EndTurn()
    {
        state = GameState.EnemyTurn;
        DrawCard(3);
        _enemyCardsController.Turn();
        state = GameState.PlayerTurn;
    }

    public void DrawCard(int count = 1)
    {
        if (cardsInDeck.Count == 0)
            return;
        for (int i = 0; i < count; i++)
        {
            if (cardsInDeck.Count > 0)
            {
                StartCoroutine((ServiceLocator.Current.Get<HandController>().DrawCardFromDeck(cardsInDeck.Last())));
                cardsInDeck.RemoveAt(cardsInDeck.Count - 1);
            }
        }
    }

    public void ShuffleDeck()
    {
        cardsInDeck = cardsInDeck.OrderBy(x => Random.value).ToList();
    }
}
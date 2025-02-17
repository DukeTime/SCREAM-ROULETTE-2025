using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGameController : MonoBehaviour, IService
{
    
    public List<GameObject> cardsInDeck;

    private EnemyCardsController _enemyCardsController;
    
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn
    }
    public GameState state;
    
    void Start()
    {
        ServiceLocator.Current.Register<CardGameController>(this);
        
        _enemyCardsController = ServiceLocator.Current.Get<EnemyCardsController>();
        ShuffleDeck();
    }

    public void DrawCard()
    {
        if (cardsInDeck.Count == 0)
            return;
        for (int i = 0; i < 3; i++)
        {
            if (cardsInDeck.Count > 0)
            {
                StartCoroutine((ServiceLocator.Current.Get<HandController>().DrawCardFromDeck(cardsInDeck.Last())));
                cardsInDeck.RemoveAt(cardsInDeck.Count - 1);
            }
        }
        EnemyTurn();
    }

    public void EnemyTurn()
    {
        state = GameState.EnemyTurn;
        _enemyCardsController.Turn();
        state = GameState.PlayerTurn;
    }

    public void ShuffleDeck()
    {
        cardsInDeck = cardsInDeck.OrderBy(x => Random.value).ToList();
    }
}
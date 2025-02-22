using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CardGameController : MonoBehaviour, IService
{
    public List<CardData> cardsInDeckData;
    public List<GameObject> cardsInHand;
    public List<ConsumableData> consumables;
    public int startCountOfCards = 5;

    public bool tutorial = false;
    public bool boss = false;
    public List<GameObject> panels;
    
    public Action GameStart;
    public Action GameEnd;
    public Action OnVictory;
    public Action OnDeath;
    public Action MonologueEnd;
    
    private EnemyCardsController _enemyCardsController;
    private EnemyCardsView _enemyCardsView;
    
    [SerializeField] private GameObject cardDefaultPrefab;
    [SerializeField] private DeckView sdfg;
    
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
        GameEnd
    }
    public GameState state;
    
    void Start()
    {
        _enemyCardsController = ServiceLocator.Current.Get<EnemyCardsController>();
        _enemyCardsController.OnAllCardsDefeated += Victory;
        
        LoadDeckCards();
        InitConsumables();
        
        StartCoroutine(EnemyMonologue());
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(1))
        //     DrawCard();
        // if (Input.GetKeyDown(KeyCode.A))
        //     ActivateConsumable(consumables[0]);
    }

    private void InitConsumables()
    {
        consumables = PlayerData.Consumables;
    }

    public void ActivateConsumable(ConsumableData consumable)
    {
        consumables.Remove(consumable);
        switch (consumable.name)
        {
            case "Earth totem":
                MakeAllHandTrump c = gameObject.AddComponent<MakeAllHandTrump>();
                c.Activate();
                return;
            case "Ritual knife":
                DestroyEnemyOpenCard d = gameObject.AddComponent<DestroyEnemyOpenCard>();
                d.Activate();
                return;
            case "Lost eye":
                ShowEnemiesCards showEnemiesCards = gameObject.AddComponent<ShowEnemiesCards>();
                showEnemiesCards.Activate();
                return;
            case "Cursed mask":
                MakeAllHand4 makeAllHand4 = gameObject.AddComponent<MakeAllHand4>();
                makeAllHand4.Activate();
                return;

        }
    }

    private void Victory()
    {
        GameEnd?.Invoke();
        OnVictory?.Invoke();
    }
    
    private void Death()
    {
        GameEnd?.Invoke();
        OnDeath?.Invoke();
    }

    private IEnumerator EnemyMonologue()
    {
        MonologueEnd?.Invoke();
        
        yield return new WaitForSeconds(1.5f);

        StartGame();
    }

    private IEnumerator Tutorial()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
            yield return null;
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
            foreach (GameObject p in panels)
                p.SetActive(false);
        }
        tutorial = false;
    }

    private void StartGame()
    {
        GameStart?.Invoke();
        
        InitConsumables();
        ShuffleDeck();
        DrawCard(startCountOfCards);
        if (tutorial)
            StartCoroutine(Tutorial());
    }

    public void EndTurn()
    {
        state = GameState.EnemyTurn;
        sdfg.View(cardsInDeckData.Count - 1);
        DrawCard(3);
        _enemyCardsController.Turn();
        state = GameState.PlayerTurn;
    }

    public void DrawCard(int count = 1)
    {
        if (cardsInDeckData.Count == 0)
        {
            state = GameState.GameEnd;
            Death();
            return;
        }
        for (int i = 0; i < count; i++)
        {
            if (cardsInDeckData.Count > 0)
            {
                GameObject cardObj = Instantiate(cardDefaultPrefab);
                cardObj.GetComponent<Card>().Init(cardsInDeckData.Last());
                
                cardsInHand.Add(cardObj);
                StartCoroutine((ServiceLocator.Current.Get<HandController>().DrawCardFromDeck(cardObj)));
                cardsInDeckData.RemoveAt(cardsInDeckData.Count - 1);
            }
        }
    }

    public void ShuffleDeck()
    {
        cardsInDeckData = cardsInDeckData.OrderBy(x => Random.value).ToList();
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void LoadDeckCards()
    {
        cardsInDeckData = PlayerData.Deck;
    }
}
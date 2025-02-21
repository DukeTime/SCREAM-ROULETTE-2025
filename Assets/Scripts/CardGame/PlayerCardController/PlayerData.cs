using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public static class PlayerData
{
    public static List<ConsumableData> StartConsumables = new List<ConsumableData>()
    {
        AllConsumables.All.Find(m => m.id == "1"),
        AllConsumables.All.Find(m => m.id == "3"),
        AllConsumables.All.Find(m => m.id == "1"),
    };
    public static List<ConsumableData> Consumables = StartConsumables;
    
    public static List<CardData> StartDeck = new List<CardData>()
    {
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 2,
                suit = CardInfo.CardSuit.Red
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 3,
                suit = CardInfo.CardSuit.Red
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 4,
                suit = CardInfo.CardSuit.Red
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 5,
                suit = CardInfo.CardSuit.Red
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 2,
                suit = CardInfo.CardSuit.Black
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 3,
                suit = CardInfo.CardSuit.Black
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 4,
                suit = CardInfo.CardSuit.Black
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 5,
                suit = CardInfo.CardSuit.Black
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 2,
                suit = CardInfo.CardSuit.Trump
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 3,
                suit = CardInfo.CardSuit.Trump
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 4,
                suit = CardInfo.CardSuit.Trump
            }
        },
        new CardData()
        {
            cardInfo = new CardInfo()
            {
                value = 5,
                suit = CardInfo.CardSuit.Trump
            }
        },
    };
    
    public static List<CardData> Deck = StartDeck;
}
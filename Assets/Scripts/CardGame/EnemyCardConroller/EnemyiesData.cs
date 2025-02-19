using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public static class EnemiesData
{
    public static List<EnemyData> all = new List<EnemyData>()
    {
        new EnemyData()
        {
            id = "1",
            name = "default cultist",
            deck = new List<EnemyCardData>()
            {
                new EnemyCardData()
                {
                    cardInfo = new CardInfo()
                    {
                        value = 8,
                        suit = CardInfo.CardSuit.Red
                    }
                },
                new EnemyCardData()
                {
                    cardInfo = new CardInfo()
                    {
                        value = 10,
                        suit = CardInfo.CardSuit.Black
                    }
                }
            }
        },
    };
}

[Serializable]
public class EnemyData
{
    public string id;
    public string name;
    public List<EnemyCardData> deck;
}
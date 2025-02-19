using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class CardData
{
    public string id;
    public CardInfo cardInfo;
}

[Serializable]
public class CardInfo
{
    public enum CardSuit
    {
        Red,
        Black,
        Trump
    };
    
    public int value = 2;
    public CardSuit suit = CardSuit.Red;
}


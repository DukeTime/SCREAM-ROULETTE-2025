using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class EnemyCardData
{
    public string id;
    public CardInfo cardInfo;

    public int Bonus => (int)(cardInfo.value == 1 ? 1 : cardInfo.value * 0.5f);
}
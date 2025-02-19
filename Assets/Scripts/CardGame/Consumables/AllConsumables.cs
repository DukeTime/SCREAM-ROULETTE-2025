using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public static class AllConsumables
{
    public static List<ConsumableData> All = new List<ConsumableData>()
    {
        new ConsumableData()
        {
            id = "1",
            name = "The Scroll of the Ancients",
            description = "Makes all the cards in the hand trump until the end of the game"
        }
    };
}
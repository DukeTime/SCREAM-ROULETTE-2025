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
            name = "Earth totem",
            description = "Temporarily makes all the cards in the hand trump"
        },
        new ConsumableData()
        {
            id = "2",
            name = "Lost eye",
            description = "Shows the opponent's cards"
        },
        new ConsumableData()
        {
            id = "3",
            name = "Ritual knife",
            description = "Destroys the opponent's face-up card"
        },
        new ConsumableData()
        {
            id = "4",
            name = "Cursed mask",
            description = "Temporarily turns the value of all the cards in your hand into 4"
        }
    };
}
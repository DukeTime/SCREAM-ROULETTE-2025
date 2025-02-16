using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    private MovebleSmoothDump moveble;
    public enum CardState
    {
        Hand,
        Played
    };
    public CardState state = CardState.Hand;
    
    void Start()
    {
        moveble = GetComponent<MovebleSmoothDump>();
    }

    void Update()
    {
        
    }

    public bool TryPlayCard(GameObject slot)
    {
        return false;
    }

}
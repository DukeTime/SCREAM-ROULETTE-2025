using UnityEngine;

public class CardInfo: MonoBehaviour
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

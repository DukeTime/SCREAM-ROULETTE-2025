public class CardInfo
{
    public enum CardSuit
    {
        Red,
        Black,
        Trump
    };
    
    public int value { get; set; } = 2;
    public CardSuit suit { get; set; } = CardSuit.Red;
    
}

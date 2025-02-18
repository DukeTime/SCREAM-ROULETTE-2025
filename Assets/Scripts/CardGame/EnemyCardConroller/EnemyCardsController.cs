using UnityEngine;

public class EnemyCardsController : MonoBehaviour, IService
{
    public GameObject[] cards;
    
    private int _cardBeated;

    private void Start()
    {
        GameStart();
        
        foreach (GameObject card in cards)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            
            enemyCard.OnBeated += CardBeated;
        }
    }

    public void GameStart()
    {
        cards[0].GetComponent<EnemyCard>().Open();
    }

    public void Turn()
    {
        foreach (GameObject card in cards)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            if (enemyCard.state == EnemyCard.EnemyCardState.Opened)
                enemyCard.GetBonus();
        }
    }

    private void CardBeated()
    {
        _cardBeated++;

        cards[_cardBeated].GetComponent<EnemyCard>().Open();
    }
}
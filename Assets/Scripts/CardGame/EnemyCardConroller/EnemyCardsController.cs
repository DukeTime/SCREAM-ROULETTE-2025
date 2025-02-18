using UnityEngine;

public class EnemyCardsController : MonoBehaviour, IService
{
    public GameObject[] cards;

    public void Turn()
    {
        foreach (GameObject card in cards)
        {
            EnemyCard enemyCard = card.GetComponent<EnemyCard>();
            if (enemyCard.state == EnemyCard.EnemyCardState.Opened)
                enemyCard.GetBonus();
        }
    }
}
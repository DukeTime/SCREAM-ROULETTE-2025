using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCardsView : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public float maxVelocity = 12.5f;

    [SerializeField] private float cardSpacing = 5f; // Расстояние между картами
    [SerializeField] private Vector2 enemyHandCenter = new Vector2(0, -7f); // Центр области руки врага
    [SerializeField] private Vector2 enemyAreaCenter = new Vector2(-2.3f, 2f); // Центр области врага
    private Vector3 velocity;
    private Vector3 currentVelocity;
    private Vector3 targetPosition = Vector3.zero;

    private CardGameController _cardGameController;
    private EnemyCardsController _enemyCardsController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _enemyCardsController = ServiceLocator.Current.Get<EnemyCardsController>();

        _cardGameController.GameStart += () => StartCoroutine(SetUpEnemyCards());
    }


    private void MoveXY(GameObject enemyCard)
    {
        if (Vector3.Distance(enemyCard.transform.position, targetPosition) > 0.01f || velocity.magnitude > 0.01f)
        {
            Vector3 newPosition = Vector3.SmoothDamp(enemyCard.transform.position, targetPosition, ref currentVelocity,
                smoothTime, maxVelocity, Time.deltaTime);
            velocity = (newPosition - enemyCard.transform.position) / Time.deltaTime;

            if (velocity.sqrMagnitude > maxVelocity * maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }

            enemyCard.transform.position = newPosition + velocity * Time.deltaTime;
            enemyCard.transform.rotation =
                Quaternion.Euler(0, 0, (enemyCard.transform.position.x - targetPosition.x) * 4);

            if (Vector3.Distance((Vector3)enemyCard.transform.position, targetPosition) < 0.01f &&
                velocity.magnitude < 0.01f)
            {
                enemyCard.transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
                velocity = Vector3.zero;
            }
        }
    }

    private IEnumerator SetUpEnemyCards()
    {
        List<GameObject> cards = _enemyCardsController.cardsObjects;
        int cardsCount = cards.Count;

        float step = cards[0].transform.localScale.x + cardSpacing;
        float spread = step * (cardsCount - 1);
        float leftStartX = enemyAreaCenter.x - spread / 2;

        for (int i = 0; i < cardsCount; i++)
        {
            GameObject cardObj = cards[i];
            velocity = Vector3.one;
            currentVelocity = Vector3.zero;
            targetPosition = new Vector3(leftStartX + step * i, enemyAreaCenter.y, 0);

            while (velocity != Vector3.zero)
            {
                MoveXY(cardObj);
                yield return null;
            }

            cardObj.GetComponent<EnemyCard>().AppearOnTheBoard();
        }
    }

    private IEnumerator ArrangeEnemyCards()
    {
        List<GameObject> cards = _enemyCardsController.cardsObjects;
        int cardsCount = cards.Count;

        float step = cards[0].transform.localScale.x + cardSpacing;
        float spread = step * (cardsCount - 1);
        float leftStartX = enemyAreaCenter.x - spread / 2;

        for (int i = 0; i < cardsCount; i++)
        {
            GameObject cardObj = cards[i];
            velocity = Vector3.one;
            currentVelocity = Vector3.zero;
            targetPosition = new Vector3(leftStartX + step * i, enemyAreaCenter.y, 0);

            while (velocity != Vector3.zero)
            {
                MoveXY(cardObj);
                yield return null;
            }
        }
    }
}
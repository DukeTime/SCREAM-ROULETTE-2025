using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxArcAngle = 60f; // Градус дуги (от -180 до 180)
    [SerializeField] private float cardSpacing = 5f; // Расстояние между картами
    [SerializeField] private float rotationAngle = 30f; // Максимальный угол поворота
    [SerializeField] private float rotationOffset = 90f;
    [SerializeField] private float verticalOffset = 0.5f; // Вертикальное смещение
    [SerializeField] private float moveSpeed = 8f; // Скорость перемещения карт
    [SerializeField] private Vector2 handCenter = new Vector2(0, -4f); // Центр области руки
    [SerializeField] private Transform deckPosition; // Позиция колоды

    public GameObject cardPrefab;
    private List<GameObject> cardsInHand = new List<GameObject>();
    private bool isAnimating;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryDrawCard();
        }
    }

    // Попытка взять карту из колоды
    private void TryDrawCard()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Deck"))
        {
            DrawCardFromDeck();
        }
    }

    // Взять новую карту из колоды
    private void DrawCardFromDeck()
    {
        GameObject newCard = Instantiate(cardPrefab, deckPosition.position, Quaternion.identity);
        cardsInHand.Add(newCard);
        StartCoroutine(ArrangeCards());
    }

    // Анимация распределения карт
    private IEnumerator ArrangeCards()
    {
        isAnimating = true;
        List<Vector3> targetPositions = CalculateCardPositions();
        List<float> targetRotations = CalculateCardRotations();

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            StartCoroutine(MoveCardToPosition(cardsInHand[i], targetPositions[i], targetRotations[i]));
        }

        yield return new WaitUntil(() => !IsAnyCardMoving());
        isAnimating = false;
    }

    private List<Vector3> CalculateCardPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        int count = cardsInHand.Count;
        
        if(count == 0) return positions;
        
        // Автоматически рассчитываем расстояние между картами
        float spread = Mathf.Min(cardSpacing, (count - 1) * 0.8f);
        float startX = -spread / 2;
        float step = (count > 1) ? spread / (count - 1) : 0;

        for(int i = 0; i < count; i++)
        {
            float x = handCenter.x + startX + i * step;
            // Параболическая формула для вертикального смещения
            float y = handCenter.y - Mathf.Pow(x - handCenter.x, 2) * 0.1f + verticalOffset;
            positions.Add(new Vector3(x, y, i * 0.01f)); // Z для порядка отрисовки
        }

        return positions;
    }

    private List<float> CalculateCardRotations()
    {
        List<float> rotations = new List<float>();
        int cardCount = cardsInHand.Count;
        
        if(cardCount == 0) return rotations;

        float totalAngle = Mathf.Clamp(maxArcAngle, 10f, 360f);
        float anglePerCard = totalAngle / (cardCount + 1);
        float startAngle = -totalAngle/2f;

        for(int i = 0; i < cardCount; i++)
        {
            float angle = startAngle + anglePerCard * (i + 1);
            // Поворачиваем карты по касательной к дуге
            float rotation = (-angle + rotationOffset) % 360;
            rotations.Add(rotation);
        }

        return rotations;
    }

    // Плавное перемещение карты
    private IEnumerator MoveCardToPosition(GameObject card, Vector3 targetPos, float targetRotation)
    {
        Collider2D collider = card.GetComponent<Collider2D>();
        if(collider != null) collider.enabled = false;

        // Сохраняем начальные значения для плавности
        Vector3 startPos = card.transform.position;
        Quaternion startRot = card.transform.rotation;
        float t = 0;

        while(t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            card.transform.position = Vector3.Lerp(startPos, targetPos, t);
            
            // Плавный поворот с учетом Z-порядка
            Quaternion targetQuat = Quaternion.Euler(0, 0, targetRotation);
            card.transform.rotation = Quaternion.Lerp(startRot, targetQuat, t);
            
            // Небольшая анимация подъема карты
            float yOffset = Mathf.Sin(t * Mathf.PI) * 0.5f;
            card.transform.position += Vector3.up * yOffset;

            yield return null;
        }

        if(collider != null) collider.enabled = true;
    }

    // Проверка движения карт
    private bool IsAnyCardMoving()
    {
        foreach (GameObject card in cardsInHand)
        {
            if (card.GetComponent<Collider2D>().enabled == false)
                return true;
        }
        return false;
    }
}
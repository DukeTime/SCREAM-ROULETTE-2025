using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Ссылка на Sprite Renderer объекта
    public GameObject player; // Ссылка на игрока
    public int baseOrder = 0; // Базовый Order in Layer

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player"); // Найти игрока по тегу
    }

    void Update()
    {
        if (player != null && spriteRenderer != null)
        {
            // Получаем Y-координаты игрока и объекта
            float playerY = player.transform.position.y;
            float objectY = transform.position.y;

            // Если объект выше игрока (игрок ниже, должен быть спереди), ставим объект на задний план
            if (objectY > playerY)
            {
                spriteRenderer.sortingOrder = baseOrder - 1; // Задний план (ниже, чтобы игрок был сверху)
            }
            // Если объект ниже игрока (игрок выше, должен быть позади), ставим на передний план
            else
            {
                spriteRenderer.sortingOrder = baseOrder + 1; // Передний план (выше, чтобы объект был сверху)
            }

            // Отладка: выводим текущие значения для проверки
            Debug.Log($"Object Y: {objectY}, Player Y: {playerY}, Sorting Order: {spriteRenderer.sortingOrder}");
        }
    }
}
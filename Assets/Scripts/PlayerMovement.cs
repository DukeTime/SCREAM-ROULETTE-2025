using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        // Получаем ввод от игрока
        float moveX = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        float moveY = Input.GetAxisRaw("Vertical");   // -1, 0, 1

        // Блокируем диагональное движение: приоритет у горизонтального ввода
        if (moveX != 0) // Если есть горизонтальный ввод
        {
            movement = new Vector2(moveX, 0); // Только горизонталь
        }
        else // Иначе берём вертикальный ввод
        {
            movement = new Vector2(0, moveY); // Только вертикаль
        }

        // Управление анимацией
        UpdateAnimation();

        // Отладка: выводим текущее состояние
        Debug.Log($"Movement: {movement}, Direction: {animator.GetInteger("Direction")}");
    }

    private void FixedUpdate()
    {
        // Применяем движение
        rb.linearVelocity = movement * moveSpeed;
    }

    private void UpdateAnimation()
    {
        // Устанавливаем анимацию в зависимости от текущего направления движения
        if (movement.magnitude > 0)
        {
            if (movement.x > 0) // Вправо
            {
                animator.SetInteger("Direction", 4);
            }
            else if (movement.x < 0) // Влево
            {
                animator.SetInteger("Direction", 3);
            }
            else if (movement.y > 0) // Вверх
            {
                animator.SetInteger("Direction", 1);
            }
            else if (movement.y < 0) // Вниз
            {
                animator.SetInteger("Direction", 2);
            }
        }
        else // Нет движения
        {
            animator.SetInteger("Direction", 0);
        }
    }
}
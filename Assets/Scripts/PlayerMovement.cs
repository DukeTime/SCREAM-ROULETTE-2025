using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private int lastDirection = 0; // 0: idle, 1: up, 2: down, 3: left, 4: right

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Получаем ввод от игрока
        float moveX = Input.GetAxisRaw("Horizontal"); // Используем GetAxisRaw для дискретного ввода
        float moveY = Input.GetAxisRaw("Vertical");

        // Ограничиваем движение по диагонали
        if (moveX != 0 && moveY != 0)
        {
            moveY = 0; // Игнорируем вертикальное движение, если есть горизонтальное
        }

        movement = new Vector2(moveX, moveY).normalized;

        // Управление анимацией
        if (movement.magnitude > 0)
        {
            if (moveY > 0) lastDirection = 1; // Вверх
            else if (moveY < 0) lastDirection = 2; // Вниз
            else if (moveX < 0) lastDirection = 3; // Влево
            else if (moveX > 0) lastDirection = 4; // Вправо
        }
        else
        {
            lastDirection = 0; // Без движения
        }

        animator.SetInteger("Direction", lastDirection);
    }

    private void FixedUpdate()
    {
        // Применяем движение
        rb.linearVelocity = movement * moveSpeed;
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerZone : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public string Game;
    public FadeManager fademanager;
    public float enemySpeed = 10f; // Скорость движения врага к игроку
    public Camera mainCamera; // Ссылка на основную камеру
    public float playerRadius = 0.5f; // Радиус модели игрока
    private GameObject player;
    private MonoBehaviour playerController;
    private Rigidbody2D playerRigidbody;
    private bool isCapturing = false; // Флаг, указывающий, что враг ловит игрока
    private Vector3 cameraOffset; // Смещение камеры относительно игрока

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerController = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();

            // Отключаем управление игроком и останавливаем его движение
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector2.zero;
                playerRigidbody.isKinematic = true;
            }

            // Включаем спрайт, если он задан
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }

            // Сохраняем смещение камеры относительно игрока
            if (mainCamera != null)
            {
                cameraOffset = mainCamera.transform.position - player.transform.position;
            }

            // Запускаем процесс поимки игрока
            StartCoroutine(CapturePlayer());
        }
    }

    private IEnumerator CapturePlayer()
    {
        isCapturing = true;

        // Отвязываем камеру от игрока
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null; // Отвязываем камеру от игрока
        }

        // Перемещаем камеру на врага
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        }

        // Ждем 2 секунды перед началом движения врага
        yield return new WaitForSeconds(2f);

        // Двигаем врага к игроку, но не ближе чем на радиус модели игрока
        while (isCapturing)
        {
            // Вычисляем направление к игроку
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Вычисляем расстояние до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Если враг находится на расстоянии больше радиуса модели игрока, двигаемся к игроку
            if (distanceToPlayer > playerRadius)
            {
                transform.Translate(direction * enemySpeed * Time.deltaTime);
            }
            else
            {
                // Если враг достиг радиуса модели игрока, останавливаемся
                isCapturing = false;
                break;
            }

            yield return null;
        }

        // Затемняем экран и загружаем сцену
        StartCoroutine(fademanager.FadeAndLoadScene(Game));
    }

    private void RestorePlayerControl()
    {
        // Восстанавливаем управление
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Восстанавливаем Rigidbody2D
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }

        // Возвращаем камеру к игроку (если нужно)
        if (mainCamera != null && player != null)
        {
            mainCamera.transform.parent = player.transform; // Привязываем камеру обратно к игроку
            mainCamera.transform.localPosition = cameraOffset; // Восстанавливаем смещение камеры
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Восстанавливаем управление и движение, если игрок вышел из зоны
            RestorePlayerControl();
        }
    }
}
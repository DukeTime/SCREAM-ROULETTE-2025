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
    public AudioClip attackSound; // Звуковой файл для нападения
    private AudioSource audioSource; // Компонент для воспроизведения звука

    private GameObject player;
    private PlayerMovement playerController; // Уточняем тип на PlayerMovement
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator; // Animator для управления анимацией
    private bool isCapturing = false; // Флаг, указывающий, что враг ловит игрока
    private Vector3 cameraOffset; // Смещение камеры относительно игрока
    private bool hasPlayedSound = false; // Флаг для воспроизведения звука один раз

    private void Start()
    {
        // Получаем или добавляем AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Убрали отключение spriteRenderer, чтобы враг был виден с самого начала
        // if (spriteRenderer != null)
        // {
        //     spriteRenderer.enabled = false;
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered trigger zone"); // Отладка: проверяем, срабатывает ли триггер

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player tag confirmed as 'Player'"); // Отладка: подтверждаем тег

            player = collision.gameObject;
            playerController = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            playerAnimator = player.GetComponent<Animator>();

            // Отключаем управление игроком и останавливаем его движение
            if (playerController != null)
            {
                playerController.enabled = false; // Отключаем скрипт движения
            }
            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector2.zero; // Сбрасываем скорость
                playerRigidbody.isKinematic = true; // Делаем тело кинематическим
            }
            if (playerAnimator != null)
            {
                playerAnimator.SetInteger("Direction", 0); // Устанавливаем состояние покоя
                playerAnimator.speed = 0; // Останавливаем анимацию
            }

            // Включаем спрайт врага (если он ещё не включён)
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true; // Это можно убрать, если враг всегда виден
            }

            // Воспроизводим звук нападения, если ещё не проигран
            if (attackSound != null && audioSource != null && !hasPlayedSound)
            {
                Debug.Log("Playing attack sound"); // Отладка: проверяем, доходит ли до воспроизведения
                audioSource.PlayOneShot(attackSound);
                hasPlayedSound = true;
            }

            // Сохраняем смещение камеры
            if (mainCamera != null)
            {
                cameraOffset = mainCamera.transform.position - player.transform.position;
            }

            // Запускаем процесс поимки
            StartCoroutine(CapturePlayer());
        }
    }

    private IEnumerator CapturePlayer()
    {
        isCapturing = true;

        // Отвязываем камеру от игрока
        if (mainCamera != null)
        {
            mainCamera.transform.parent = null;
        }

        // Плавно перемещаем камеру к врагу
        if (mainCamera != null)
        {
            Vector3 targetCameraPosition = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            float cameraMoveTime = 1f;
            float elapsedTime = 0f;
            Vector3 startCameraPosition = mainCamera.transform.position;

            while (elapsedTime < cameraMoveTime)
            {
                elapsedTime += Time.deltaTime;
                mainCamera.transform.position = Vector3.Lerp(startCameraPosition, targetCameraPosition, elapsedTime / cameraMoveTime);
                yield return null;
            }
            mainCamera.transform.position = targetCameraPosition;
        }

        // Ждем перед началом движения врага
        yield return new WaitForSeconds(2f);

        // Двигаем врага к игроку
        while (isCapturing)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > playerRadius)
            {
                transform.Translate(direction * enemySpeed * Time.deltaTime);
            }
            else
            {
                isCapturing = false;
                break;
            }

            yield return null;
        }

        // Затемнение и загрузка сцены
        StartCoroutine(fademanager.FadeAndLoadScene(Game));
    }

    private void RestorePlayerControl()
    {
        // Восстанавливаем управление игроком
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }
        if (playerAnimator != null)
        {
            playerAnimator.speed = 1; // Возобновляем анимацию
        }

        // Привязываем камеру обратно к игроку
        if (mainCamera != null && player != null)
        {
            mainCamera.transform.parent = player.transform;
            mainCamera.transform.localPosition = cameraOffset;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Останавливаем процесс поимки
            StopCoroutine(CapturePlayer());
            isCapturing = false;

            // Восстанавливаем управление
            RestorePlayerControl();

            // Сбрасываем флаг звука при выходе
            hasPlayedSound = false;

            // Выключаем спрайт врага (если нужно, чтобы он исчезал при выходе)
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false; // Оставляем или убираем в зависимости от логики
            }
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Altar : MonoBehaviour
{
    public int id = 0;
    public string Upgrade; // Имя сцены для загрузки
    public GameObject pressEText; // Ссылка на текстовый объект UI, который будет отображать "Press E"
    public ScreenShadowing fademanager;
    private bool isPlayerInRange = false;
    private GameObject player; // Ссылка на игрока
    private MonoBehaviour playerController; // Компонент управления игроком
    private Rigidbody2D playerRigidbody; // Компонент Rigidbody2D игрока

    private void Start()
    {
        if (id < GameStateManager.Instance.altars)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pressEText.SetActive(true); // Показываем текст "Press E"

            // Сохраняем ссылку на игрока
            player = collision.gameObject;

            // Находим компонент управления игроком
            playerController = player.GetComponent<PlayerMovement>();

            // Находим компонент Rigidbody2D игрока
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pressEText.SetActive(false); // Скрываем текст "Press E"

            // Восстанавливаем управление и движение, если игрок вышел из зоны
            RestorePlayerControl();
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameStateManager.Instance.playerPosition = transform.position;
            GameStateManager.Instance.altars++;
            
            // Блокируем управление и останавливаем движение
            DisablePlayerControl();

            // Запускаем загрузку сцены с эффектом затемнения
            StartCoroutine(LoadSceneWithFade());
        }
    }

    private void DisablePlayerControl()
    {
        // Если компонент управления найден, отключаем его
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Если Rigidbody2D найден, останавливаем движение
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector2.zero; // Обнуляем скорость
            playerRigidbody.isKinematic = true; // Делаем объект кинематическим
        }
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
            playerRigidbody.isKinematic = false; // Возвращаем физическое поведение
        }
    }

    private IEnumerator LoadSceneWithFade()
    {
        // Запускаем эффект затемнения
        StartCoroutine(fademanager.ChangeScene(6));
        yield return new WaitForSeconds(1f);

        // Если загрузка сцены не произошла, восстанавливаем управление и движение
        if (!string.IsNullOrEmpty(Upgrade))
        {
            // Если сцена загружена, управление и движение не нужно восстанавливать
            yield break;
        }

        // Если загрузка сцены не произошла, восстанавливаем управление и движение
        RestorePlayerControl();
    }
}
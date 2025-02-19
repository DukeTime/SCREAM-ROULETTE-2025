using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class Altar : MonoBehaviour
{
    public string Upgrade; // Имя сцены для загрузки
    public GameObject pressEText; // Ссылка на текстовый объект UI, который будет отображать "Press E"
    public FadeManager fademanager;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pressEText.SetActive(true); // Показываем текст "Press E"
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pressEText.SetActive(false); // Скрываем текст "Press E"
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(fademanager.FadeAndLoadScene(Upgrade));
        }
    }

}
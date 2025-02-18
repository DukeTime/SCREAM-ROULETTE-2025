using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Altar : MonoBehaviour
{
    public string Upgrade; 
    public GameObject pressEText; 

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pressEText.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pressEText.SetActive(false); 
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(Upgrade))
        {
            SceneManager.LoadScene(Upgrade);
        }
    }
}
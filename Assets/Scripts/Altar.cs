using UnityEngine;
using UnityEngine.SceneManagement;

public class Altar : MonoBehaviour
{
    public string Upgrade = "Upgrade";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("LoadNextScene", 2f);
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
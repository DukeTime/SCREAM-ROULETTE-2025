using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public string Game;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }


            Invoke("LoadNextScene", 2f);
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(Game))
        {
            SceneManager.LoadScene(Game);
        }
    }
}
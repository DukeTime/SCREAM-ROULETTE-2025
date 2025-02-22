using UnityEngine;
using UnityEngine.SceneManagement;


public class Bootstrap: MonoBehaviour
{
    private void Start()
    {
        PlayerData.Deck = PlayerData.StartDeck;
        PlayerData.Consumables = PlayerData.StartConsumables;
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.Reset();
        
        SceneManager.LoadScene(5);
    }
}
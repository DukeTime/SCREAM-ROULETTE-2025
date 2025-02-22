using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bootstrap: MonoBehaviour
{
    private void Start()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.Reset();
        
        PlayerData.Deck = PlayerData.StartDeck;
        PlayerData.Consumables = new List<ConsumableData>();
        
        SceneManager.LoadScene(5);
    }
}
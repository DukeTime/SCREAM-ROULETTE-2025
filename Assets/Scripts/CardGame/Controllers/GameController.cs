using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    public Vector3 playerPosition;
    public int enemies;
    public int altars;// Уникальный идентификатор врага
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Instance.Reset();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Reset()
    {
        playerPosition = new Vector3(-1, 0, 0);
        enemies = 0;
        altars = 0;
    }
}
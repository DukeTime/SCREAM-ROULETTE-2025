using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public ScreenShadowing fademanager;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(fademanager.ChangeScene(9));
    }
}
using UnityEngine;

public class GameController : MonoBehaviour, IService
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ServiceLocator.Current.Register<GameController>(this);
        Debug.Log("Game launcded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
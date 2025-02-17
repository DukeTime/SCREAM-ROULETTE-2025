using UnityEngine;

public class EnemyCardsController : MonoBehaviour, IService
{
    private static EnemyCardsController instance;
    
    public GameObject[] cards;
    
    void Start()
    {
        instance = this;
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register<EnemyCardsController>(this);
    }

    public void Turn()
    {
        
    }
}
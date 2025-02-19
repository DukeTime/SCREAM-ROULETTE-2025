using UnityEngine;
using UnityEngine.InputSystem;

public class CardGameLoader : MonoBehaviour
{
    [SerializeField] private CardGameController cardGameController;
    [SerializeField] private HandController handController;
    [SerializeField] private EnemyCardsController enemyCardsController;
    [SerializeField] private PlayerInput playerInput;
    private void Awake()
    {
        ServiceLocator.Initialize();
        
        ServiceLocator.Current.Register<CardGameController>(cardGameController);
        ServiceLocator.Current.Register<HandController>(handController);
        ServiceLocator.Current.Register<EnemyCardsController>(enemyCardsController);
    }
}
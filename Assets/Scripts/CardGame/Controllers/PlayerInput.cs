using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CardGameController _cardGameController;
    private HandController _handController;
    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _handController = ServiceLocator.Current.Get<HandController>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryDrawCard();
        }
    }

    private void TryDrawCard()
    {
        if (_handController.isAnimating)
            return;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Deck"))
            _cardGameController.EndTurn();
    }
}
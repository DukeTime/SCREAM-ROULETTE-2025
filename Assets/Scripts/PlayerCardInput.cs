using UnityEngine;

public class PlayerCardInput : MonoBehaviour
{
    private CardGameController _cardGameController;
    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Deck"))
            _cardGameController.DrawCard();
    }
}
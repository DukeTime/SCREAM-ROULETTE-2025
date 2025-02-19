using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardView : MonoBehaviour
{
    private Card _cardController;
    private CardInfo _cardData;
    
    [SerializeField] private SpriteRenderer suitIcon;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        _cardController = GetComponent<Card>();
        _cardData = _cardController.cardData.cardInfo;
        
        SynchronizeView();
    }

    private void SynchronizeView()
    {
        suitIcon.color = _cardData.suit == CardInfo.CardSuit.Red ? Color.red : _cardData.suit == CardInfo.CardSuit.Black ? Color.black : Color.magenta;
        valueText.text = _cardData.value.ToString();
    }
}
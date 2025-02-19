using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardView : MonoBehaviour
{
    private Card cardController;
    private CardInfo cardInfo;
    
    [SerializeField] private SpriteRenderer suitIcon;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        cardController = GetComponent<Card>();
        cardInfo = GetComponent<CardInfo>();
        
        SynchronizeView();
    }

    private void SynchronizeView()
    {
        suitIcon.color = cardInfo.suit == CardInfo.CardSuit.Red ? Color.red : cardInfo.suit == CardInfo.CardSuit.Black ? Color.black : Color.magenta;
        valueText.text = cardInfo.value.ToString();
    }
}
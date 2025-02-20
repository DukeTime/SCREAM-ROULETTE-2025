using System;
using DG.Tweening;
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
    
    [SerializeField] private Sprite redSuitSprite;
    [SerializeField] private Sprite blackSuitSprite;
    [SerializeField] private Sprite trumpSuitSprite;

    private void Start()
    {
        _cardController = GetComponent<Card>();
        _cardData = _cardController.cardData.cardInfo;
        
        SynchronizeUI();
    }

    private void SynchronizeUI()
    {
        suitIcon.sprite = _cardData.suit == CardInfo.CardSuit.Red ? redSuitSprite : 
            _cardData.suit == CardInfo.CardSuit.Black ? blackSuitSprite : trumpSuitSprite;
        valueText.text = _cardData.value.ToString();
    }

    public void SynchronizeNewView()
    {
        SynchronizeUI();
        transform.DOPunchScale(Vector3.one * 0.4f, 0.5f);
    }
}
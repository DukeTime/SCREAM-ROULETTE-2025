using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCardView : MonoBehaviour
{
    private EnemyCard _enemyCardController;
    private EnemyCardData _cardData;
    private Animator _animator;
    
    [SerializeField] private SpriteRenderer suitIcon;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        _enemyCardController = GetComponent<EnemyCard>();
        _cardData = _enemyCardController.cardData;
        _animator = GetComponent<Animator>();

        _enemyCardController.OnPlayCard += PlayCard;
        _enemyCardController.OnBeated += Beat;
        _enemyCardController.OnGetBonus += GetBonus;
        _enemyCardController.OnOpened += Open;
        
        SynchronizeView();
    }

    private void SynchronizeView()
    {
        suitIcon.color = _cardData.cardInfo.suit == CardInfo.CardSuit.Red ? Color.red : 
            _cardData.cardInfo.suit == CardInfo.CardSuit.Black ? Color.black : Color.magenta;
        valueText.text = _cardData.cardInfo.value.ToString();
    }

    private void Open()
    {
        _animator.SetTrigger("Open");
    }
    
    private void PlayCard()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.5f, 0.2f)
            .OnKill(() => {
            valueText.text = _cardData.cardInfo.value.ToString();
        });
    }
    
    private void Beat()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.5f, 0.2f)
            .OnKill(() => {
                valueText.text = _cardData.cardInfo.value.ToString();
            });
    }
    
    private void GetBonus()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.5f, 0.2f)
            .OnKill(() => {
                valueText.text = _cardData.cardInfo.value.ToString();
            });
    }
}
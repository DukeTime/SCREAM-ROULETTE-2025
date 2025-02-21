using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private SpriteRenderer weaponIcon;
    
    [SerializeField] private Sprite redSuitSprite;
    [SerializeField] private Sprite blackSuitSprite;
    [SerializeField] private Sprite trumpSuitSprite;
    [SerializeField] private List<Sprite> weaponSprites = new List<Sprite>(4);

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
        string spriteText = _cardData.cardInfo.suit == CardInfo.CardSuit.Red ? "<sprite name=\"Red\">" : 
            _cardData.cardInfo.suit == CardInfo.CardSuit.Black ? "<sprite name=\"Black\">" : "<sprite name=\"Trump\">";
        valueText.text = _cardData.cardInfo.value.ToString() + spriteText;

        switch (_cardData.cardInfo.value)
        {
            case < 8:
                weaponIcon.sprite = weaponSprites[0];
                return;
            case < 10:
                weaponIcon.sprite = weaponSprites[1];
                return;
            case < 13:
                weaponIcon.sprite = weaponSprites[2];
                return;
            default:
                weaponIcon.sprite = weaponSprites[3];
                return;
        }
    }

    private void Open()
    {
        _animator.SetTrigger("Open");
    }
    
    private void PlayCard()
    {
        valueText.transform.DOPunchScale(Vector3.one * -0.1f, 0.2f);
        valueText.transform.DOPunchPosition( Vector3.down * 0.075f, 0.2f, 25, 2f)
            .OnKill(SynchronizeView);
    }
    
    private void Beat()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.5f, 0.2f)
            .OnKill(SynchronizeView);
    }
    
    private void GetBonus()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.5f, 0.2f)
            .OnKill(SynchronizeView);
    }
}
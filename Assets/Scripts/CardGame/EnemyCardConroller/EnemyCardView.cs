using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCardView : MonoBehaviour
{
    private EnemyCard enemyCardController;
    private CardInfo cardInfo;
    
    [SerializeField] private SpriteRenderer suitIcon;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        enemyCardController = GetComponent<EnemyCard>();
        cardInfo = GetComponent<CardInfo>();

        enemyCardController.OnPlayCard += PlayCard;
        enemyCardController.OnBeated += Beat;
        enemyCardController.OnGetBonus += GetBonus;
        
        SynchronizeView();
    }

    private void SynchronizeView()
    {
        suitIcon.color = cardInfo.suit == CardInfo.CardSuit.Red ? Color.red : cardInfo.suit == CardInfo.CardSuit.Black ? Color.black : Color.magenta;
        valueText.text = cardInfo.value.ToString();
    }
    
    private void PlayCard()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.3f, 0.2f)
            .OnComplete(() => {
            valueText.text = cardInfo.value.ToString();
        });
    }
    
    private void Beat()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.3f, 0.2f)
            .OnComplete(() => {
                valueText.text = cardInfo.value.ToString();
            });
    }
    
    private void GetBonus()
    {
        valueText.transform.DOPunchScale( Vector3.one * 0.3f, 0.2f)
            .OnComplete(() => {
                valueText.text = cardInfo.value.ToString();
            });
    }
}
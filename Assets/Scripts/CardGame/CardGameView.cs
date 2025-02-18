using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardGameView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private CardGameController _cardGameController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        
        _cardGameController.MonologueEnd += () => StartCoroutine(GameStart());
        _cardGameController.GameEnd += () => StartCoroutine(GameEnd());
    }

    private IEnumerator GameStart()
    {
        _animator.SetTrigger("GameStart");

        yield return null;
    }
    
    private IEnumerator GameEnd()
    {
        _animator.SetTrigger("GameEnd");
        
        yield return null;
    }
}
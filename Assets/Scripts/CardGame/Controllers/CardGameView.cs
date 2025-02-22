using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CardGameView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;
    private CardGameController _cardGameController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        
        _cardGameController.MonologueEnd += () => StartCoroutine(GameStart());
        _cardGameController.OnVictory += () => StartCoroutine(Victory());
        _cardGameController.OnDeath += () => StartCoroutine(Death());
    }

    private IEnumerator GameStart()
    {
        _animator.SetTrigger("GameStart");

        yield return null;
    }
    
    private IEnumerator Victory()
    {
        _animator.SetTrigger("Victory");
        
        yield return new WaitForSeconds(4.5f);
        
        if (_cardGameController.boss)
            _cardGameController.ChangeScene("FinalScene");
        else
            _cardGameController.ChangeScene("Map1");
    }
    
    private IEnumerator Death()
    {
        _animator.SetTrigger("Death");
        
        yield return new WaitForSeconds(1.3f);
        
        _audioSource1.Play();
        
        yield return new WaitForSeconds(2f);
        
        _audioSource2.Play();
        
        yield return new WaitForSeconds(7.5f);
        
        _cardGameController.ChangeScene("GameOver");
    }
}
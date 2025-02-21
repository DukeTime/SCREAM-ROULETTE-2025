using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenShadowing : MonoBehaviour
{
    private Animator _animator;
    private GameController _gameController;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public IEnumerator ChangeScene(int sceneName)
    {
        _animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}

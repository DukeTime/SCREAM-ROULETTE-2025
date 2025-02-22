using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene: MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(9);
    }
}
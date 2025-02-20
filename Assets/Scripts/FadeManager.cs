using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    public Animator fadeAnimator;

    public IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Проверяем, что fadeAnimator не уничтожен
        if (fadeAnimator == null)
        {
            Debug.LogError("FadeAnimator is missing!");
            yield break;
        }

        // Запускаем анимацию затемнения
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);

        // Асинхронно загружаем следующую сцену
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Запускаем анимацию растемнения
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
    }
}
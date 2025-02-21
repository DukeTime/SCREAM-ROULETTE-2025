using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource audioSource;
    public AudioClip defaultMusic; // Музыка по умолчанию для других сцен
    public AudioClip gameMusic; // Музыка для сцены Game
    public string gameSceneName = "Game"; // Имя сцены, где будет играть gameMusic

    void Awake()
    {
        // Если экземпляр уже существует, уничтожаем дубликат
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Сохраняем этот объект между сценами
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

        // Убеждаемся, что AudioSource существует
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Устанавливаем музыку по умолчанию, если она есть
        if (defaultMusic != null && audioSource != null)
        {
            audioSource.clip = defaultMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Проверяем, загрузилась ли сцена Game
        if (scene.name == gameSceneName && gameMusic != null)
        {
            // Останавливаем текущую музыку
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Устанавливаем и воспроизводим музыку для сцены Game
            audioSource.clip = gameMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (defaultMusic != null)
        {
            // Если это не сцена Game, воспроизводим музыку по умолчанию
            if (audioSource.clip != defaultMusic || !audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = defaultMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
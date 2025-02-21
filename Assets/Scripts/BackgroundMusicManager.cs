using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource audioSource;

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
    }

    void Start()
    {
        // Убедимся, что музыка воспроизводится и циклично
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            audioSource.loop = true;
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
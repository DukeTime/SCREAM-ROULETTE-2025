using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource audioSource;
    public AudioClip defaultMusic; // ������ �� ��������� ��� ������ ����
    public AudioClip gameMusic; // ������ ��� ����� Game
    public string gameSceneName = "Game"; // ��� �����, ��� ����� ������ gameMusic

    void Awake()
    {
        // ���� ��������� ��� ����������, ���������� ��������
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ��������� ���� ������ ����� �������
        instance = this;
        DontDestroyOnLoad(gameObject);

        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();

        // ����������, ��� AudioSource ����������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ������������� �� ������� �������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // ������������� ������ �� ���������, ���� ��� ����
        if (defaultMusic != null && audioSource != null)
        {
            audioSource.clip = defaultMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���������, ����������� �� ����� Game
        if (scene.name == gameSceneName && gameMusic != null)
        {
            // ������������� ������� ������
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // ������������� � ������������� ������ ��� ����� Game
            audioSource.clip = gameMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (defaultMusic != null)
        {
            // ���� ��� �� ����� Game, ������������� ������ �� ���������
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
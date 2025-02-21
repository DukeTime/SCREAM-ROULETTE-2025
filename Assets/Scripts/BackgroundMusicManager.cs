using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource audioSource;

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
    }

    void Start()
    {
        // ��������, ��� ������ ��������������� � ��������
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
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UITextShake : MonoBehaviour
{
    [Tooltip("Максимальное смещение в пикселях")]
    public float amplitude = 1.5f;
    
    [Tooltip("Скорость изменения эффекта")]
    public float speed = 3f;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Vector2 noiseOffset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;
        noiseOffset = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    }

    void Update()
    {
        // Генерация плавных смещений с помощью Perlin Noise
        float x = (Mathf.PerlinNoise(Time.time * speed + noiseOffset.x, noiseOffset.y) * 2 - 1) * amplitude;
        float y = (Mathf.PerlinNoise(noiseOffset.x, Time.time * speed + noiseOffset.y) * 2 - 1) * amplitude;
        
        // Применение смещения к позиции
        rectTransform.localPosition = originalPosition + new Vector3(x, y, 0);
    }

    // Для временного отключения эффекта
    public void SetShakeEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (!enabled) rectTransform.localPosition = originalPosition;
    }
}
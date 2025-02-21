using UnityEngine;
using UnityEngine.UI;

public class ConsumableChoiceSlot : MonoBehaviour 
{
    [SerializeField] private Image iconImage;
    private ConsumableData _consumableData;
    private ConsumableChoicePanel _panel;

    public void Initialize(ConsumableData data, ConsumableChoicePanel panel) {
        _consumableData = data;
        _panel = panel;
        iconImage.sprite = LoadSprite(data.name);
    }
    
    public void OnClick() {
        _panel.ShowDescription(_consumableData, -transform.localPosition, this);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
    
    public Sprite LoadSprite(string spriteName)
    {
        // Путь относительно папки Resources без расширения файла
        string path = "Images/" + spriteName;
        
        // Загрузка спрайта
        Sprite loadedSprite = Resources.Load<Sprite>(path);
        
        if (loadedSprite != null)
        {
            Debug.Log("Спрайт загружен успешно!");
            // Используйте loadedSprite (например: GetComponent<SpriteRenderer>().sprite = loadedSprite)
        }
        else
        {
            Debug.LogError($"Спрайт '{spriteName}' не найден по пути: {path}");
        }

        return loadedSprite;
    }
}
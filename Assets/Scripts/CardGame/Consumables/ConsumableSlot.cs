using UnityEngine;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour 
{
    [SerializeField] private Image iconImage;
    private ConsumableData _consumableData;
    private ConsumablePanel _panel;

    public void Initialize(ConsumableData data, ConsumablePanel panel) {
        _consumableData = data;
        _panel = panel;
        iconImage.sprite = data.icon;
    }
    
    public void OnClick() {
        _panel.ShowDescription(_consumableData, -transform.localPosition, this);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
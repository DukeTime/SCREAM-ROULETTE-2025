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
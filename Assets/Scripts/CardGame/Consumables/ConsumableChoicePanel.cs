using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class ConsumableChoicePanel : MonoBehaviour {
    
    [SerializeField] private GameObject consumableSlotPrefab;
    [SerializeField] private ConsumableChoiceDescriptionPanel consumableDescriptionPanel;
    [SerializeField] private List<ConsumableData> consumableDatas;
    
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        if (consumableDatas.Count == 0)
        {
            _image.enabled = false;
            return;
        }
        else
            _image.enabled = true;
        
        foreach (Transform child in transform) Destroy(child.gameObject);
        
        foreach (var consumableData in consumableDatas) {
            var slot = Instantiate(consumableSlotPrefab, transform);
            slot.GetComponent<ConsumableChoiceSlot>().Initialize(consumableData, this);
        }
    }
    
    public void ShowDescription(ConsumableData data, Vector2 position, ConsumableChoiceSlot slot) {
        consumableDescriptionPanel.Show(data, position, slot);
    }

    public void BlockAllButtons()
    {
        foreach (Transform child in transform) 
            child.GetComponent<Button>().enabled = false;
    }
}
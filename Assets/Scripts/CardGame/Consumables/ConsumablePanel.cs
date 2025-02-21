using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class ConsumablePanel : MonoBehaviour {
    
    [SerializeField] private GameObject consumableSlotPrefab;
    [SerializeField] private ConsumableDescriptionPanel consumableDescriptionPanel;
    [SerializeField] private List<ConsumableData> consumableDatas;
    
    private CardGameController _cardGameController;
    private Image _image;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _cardGameController.GameStart += StartGame;
        _image = GetComponent<Image>();
    }

    private void StartGame()
    {
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        consumableDatas = _cardGameController.consumables;
        
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
            slot.GetComponent<ConsumableSlot>().Initialize(consumableData, this);
        }
    }
    
    public void ShowDescription(ConsumableData data, Vector2 position, ConsumableSlot slot) {
        consumableDescriptionPanel.Show(data, position, slot);
    }
}
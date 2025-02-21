using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using System;          // Для доступа к Random.Shared
using System.Linq;     // Для методов LINQ (OrderBy, Take, Enumerable.Range и Select)
using System.Collections.Generic;

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
        List<string> indexes = new List<string>() { "4", "1", "2", "3"};
        string ind1 = indexes[Random.Range(0, indexes.Count)];
        indexes.Remove(ind1);
        string ind2 = indexes[Random.Range(0, indexes.Count)];
        indexes.Remove(ind2);
        consumableDatas.Add(AllConsumables.All.Find(m => m.id == ind1));
        consumableDatas.Add(AllConsumables.All.Find(m => m.id == ind2));
        Debug.Log(consumableDatas);
        
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
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableDescriptionPanel : MonoBehaviour 
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button useButton;
    
    private CardGameController _cardGameController;

    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
    }

    public void Show(ConsumableData data, Vector2 position, ConsumableSlot slot) {
        transform.localPosition = new Vector2(-573.8184f, Mathf.Max(-440, position.y - 540));
        gameObject.SetActive(true);
        
        nameText.text = data.name;
        descriptionText.text = data.description;
        
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => {
            _cardGameController.ActivateConsumable(data);
            slot.Delete();
            gameObject.SetActive(false);
        });
    }

    private void Disappear()
    {
        
    }
}
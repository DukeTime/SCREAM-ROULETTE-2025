using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableDescriptionPanel : MonoBehaviour 
{
    [SerializeField] private ConsumablePanel consumablePanel;
    [SerializeField] private GameObject background; 
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button useButton;

    private Image _panelImage;
    private CardGameController _cardGameController;

    private void Awake() 
    {
        background.GetComponent<Button>().onClick.AddListener(Disappear);
    }
    
    private void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        _panelImage = GetComponent<Image>();
        
        Disappear();
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0) && gameObject.activeSelf)
    //     {
    //         Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    //
    //         if (hit.collider != null && !hit.collider.CompareTag("ConsumablePanel"))
    //             return;
    //         Disappear();
    //     }
    // }

    public void Show(ConsumableData data, Vector2 position, ConsumableSlot slot) {
        transform.localPosition = new Vector2(-573.8184f, Mathf.Max(-440, position.y - 540));
        Appear();
        nameText.text = data.name;
        descriptionText.text = data.description;
        
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => {
            _cardGameController.ActivateConsumable(data);
            if (PlayerData.Consumables.Count == 0)
                consumablePanel.gameObject.GetComponent<Image>().enabled = false; 
            slot.Delete();
            Disappear();
        });
    }

    public void Disappear()
    {
        background.SetActive(false);
        gameObject.SetActive(false);
    }
    
    private void Appear()
    {
        gameObject.SetActive(true);
        background.SetActive(true);
    }
}
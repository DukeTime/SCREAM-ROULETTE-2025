using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableChoiceDescriptionPanel : MonoBehaviour 
{
    [SerializeField] private ConsumableChoicePanel consumablePanel;
    [SerializeField] private ScreenShadowing fadeManager;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button useButton;

    private Image _panelImage;
    
    private void Start()
    {
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

    public void Show(ConsumableData data, Vector2 position, ConsumableChoiceSlot slot) {
        transform.localPosition = new Vector2(position.x, -90f);
        Appear();
        nameText.text = data.name;
        descriptionText.text = data.description;
        
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => {
            consumablePanel.BlockAllButtons();
            PlayerData.Consumables.Add(data);
            StartCoroutine(fadeManager.ChangeScene(1));
            //slot.Delete();
            //Disappear();
        });
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
    
    private void Appear()
    {
        gameObject.SetActive(true);
    }
}
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MovebleSmoothDump : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    
    public float smoothTime = 0.3f;
    public float maxVelocity = 12.5f;
    public bool isHolding = false;
    public bool isSelected = false;
    public bool isDraggingAllowed = true;
    public bool isAnimating = false;
    
    private Vector3 velocity;
    private Vector3 currentVelocity;
    
    private Card cardConroller;
    private Camera mainCamera;
    
    private static Vector3 DEFAULT_SCALE = new Vector3(1.92f, 2.7072f, 1);
    private static Vector3 SELECTED_SCALE = new Vector3(1.92f * 1.5f, 2.7072f * 1.5f, 1);
    
    private Collider2D _triggerCollider;
    private CardGameController _cardGameController;
    void Start()
    {
        _cardGameController = ServiceLocator.Current.Get<CardGameController>();
        cardConroller = GetComponent<Card>();
        _triggerCollider = GetComponent<Collider2D>();
        targetPosition = transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isHolding)
            MoveXY(smoothTime);
    }

    private void MoveXY(float smoothTimeToMove)
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f || velocity.magnitude > 0.01f)
        {
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity,
                smoothTimeToMove, maxVelocity, Time.deltaTime);
            velocity = (newPosition - transform.position) / Time.deltaTime;

            if (velocity.sqrMagnitude > maxVelocity * maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }
            
            transform.position = newPosition + velocity * Time.deltaTime;
            transform.rotation =
                Quaternion.Euler(0, 0, (transform.position.x - targetPosition.x) * 4);
            
            if (Vector3.Distance((Vector3)transform.position, targetPosition) < 0.01f && velocity.magnitude < 0.01f)
            {
                transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
                velocity = Vector3.zero;
            }
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        targetPosition = mainCamera.ScreenToWorldPoint(mousePos);
        targetPosition.z = -1;
    }

    public void OnMouseUp()
    {
        isHolding = false;
        Bounds boundsA = _triggerCollider.bounds;
    
        Collider[] overlaps = Physics.OverlapBox(
            boundsA.center, 
            boundsA.extents, 
            _triggerCollider.transform.rotation, 
            Physics.AllLayers, 
            QueryTriggerInteraction.Collide
        );

        bool returnToHandFlag = true;
        foreach (Collider col in overlaps)
        {
            if (col != null && col.CompareTag("CardSlot"))
            {
                if (cardConroller.TryPlayCard(col.gameObject))
                {
                    returnToHandFlag = false;
                    isDraggingAllowed = false;
                    StartCoroutine(MoveOnEnemy(col.gameObject));
                }
            }
        }
        if (returnToHandFlag)
            StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
    }

    public void OnMouseDown()
    {
        if (!_cardGameController.tutorial)
        {
            if (isDraggingAllowed)
            {
                isHolding = true;
                StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());

            }
        }
    }

    private IEnumerator MoveOnEnemy(GameObject enemyCard)
    {
        isAnimating = true;
        int cardsOnCount = enemyCard.GetComponent<EnemyCard>().playerCardsOn.Count();
        cardsOnCount = 1;
                    
        targetPosition = new Vector3(enemyCard.transform.position.x, 
            enemyCard.transform.position.y - transform.localScale.y / 3 * cardsOnCount, 
            -1);
        while (velocity != Vector3.zero)
        {
            MoveXY(smoothTimeToMove: smoothTime / 4);
            yield return null;
        }
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -0.01f * cardsOnCount);
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, -0.01f * cardsOnCount);
        
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        
        isAnimating = false;
    }
    
    // public void SelectedView()
    // {
    //     isSelected = true;
    //     transform.localScale = SELECTED_SCALE;
    //     transform.rotation = Quaternion.Euler(Vector3.zero);
    //     targetPosition.z = -1;
    // }
    //
    // public void SelectedUnview()
    // {
    //     if (isSelected)
    //     {
    //         isSelected = false;
    //         transform.localScale = DEFAULT_SCALE;
    //         StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
    //     }
    // }
}
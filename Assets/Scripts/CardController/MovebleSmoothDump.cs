using System;
using UnityEngine;

public class MovebleSmoothDump : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    
    public float smoothTime = 0.3f;
    public float maxVelocity = 12.5f;
    public bool isHolding = false;
    public bool isSelected = false;
    
    private Vector3 velocity;
    private Vector3 currentVelocity;
    
    private Card cardConroller;
    private Camera mainCamera;
    
    private static Vector3 DEFAULT_SCALE = new Vector3(1.92f, 2.7072f, 1);
    private static Vector3 SELECTED_SCALE = new Vector3(1.92f * 1.5f, 2.7072f * 1.5f, 1);
    
    private Collider2D collider;
    void Start()
    {
        cardConroller = GetComponent<Card>();
        collider = GetComponent<Collider2D>();
        targetPosition = transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        MoveXY();
    }

    protected void MoveXY()
    {
        if (isHolding)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f || velocity.magnitude > 0.01f)
            {
                Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity,
                    smoothTime, maxVelocity, Time.deltaTime);
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
        Bounds boundsA = collider.bounds;
    
        Collider[] overlaps = Physics.OverlapBox(
            boundsA.center, 
            boundsA.extents, 
            collider.transform.rotation, 
            Physics.AllLayers, 
            QueryTriggerInteraction.Collide
        );

        bool returnToHandFlag = true;
        foreach (Collider collider in overlaps)
        {
            if (collider != null && collider.CompareTag("CardSlot"))
            {
                if (cardConroller.TryPlayCard(collider.gameObject))
                {
                    returnToHandFlag = false;
                    Destroy(gameObject);
                }
            }
        }
        if (returnToHandFlag)
            StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
    }
    public void OnMouseDown()
    {
        isHolding = true;
        StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
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
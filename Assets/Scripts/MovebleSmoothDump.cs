using System;
using UnityEngine;

public class MovebleSmoothDump : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 globalTargetPosition = Vector3.zero;
    
    public float smoothTime = 0.3f;
    public float maxVelocity = 12.5f;
    public bool isHolding = false;
    
    private Vector3 velocity;
    private Vector3 currentVelocity;
    
    private Card cardConroller;
    private Camera mainCamera;
    
    void Start()
    {
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
                    Quaternion.Euler(0, 0, transform.position.x - targetPosition.x * 4);
                
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
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnMouseUp()
    {
        isHolding = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("CardSlot"))
        {
            if (cardConroller.TryPlayCard(hit.collider.gameObject))
            {
                
            }
            else
            {
                StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
            }
        }
        else
        {
            StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
        }
        
    }
    public void OnMouseDown()
    {
        isHolding = true;
        StartCoroutine(ServiceLocator.Current.Get<HandController>().ArrangeCards());
    }
}
using System;
using UnityEngine;

public class MovebleSmoothDump : MonoBehaviour
{
    private Vector3 velocity;
    public float smoothTime = 0.3f;
    public float maxVelocity = 12.5f;
    private Vector3 currentVelocity;
    public Vector3 targetPosition = Vector3.zero;
    private Camera mainCamera;
    public bool isHolding = false;
    
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
        targetPosition.z = 0;
        isHolding = true;
    }

    public void OnMouseUp()
    {
        isHolding = false;
    }
}
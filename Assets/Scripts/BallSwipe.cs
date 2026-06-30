using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwipe : MonoBehaviour
{
    public float forceMultiplier = 3f;

    private Rigidbody rb;
    private Vector2 startMousePos;
    private Vector2 endMousePos;
    private bool isThrown = false;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ScoreManager.Instance != null) ScoreManager.Instance.EndThrow();
        }

        if (isThrown) return;

        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endMousePos = Input.mousePosition;
            Vector2 swipeDirection = endMousePos - startMousePos;
            Vector3 throwForce = new Vector3(swipeDirection.x, 0, swipeDirection.y);
            rb.AddForce(throwForce * forceMultiplier);
            isThrown = true;
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition; 
        rb.velocity = Vector3.zero;   
        rb.angularVelocity = Vector3.zero;  
        isThrown = false;                  
    }
}
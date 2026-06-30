using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public bool isFallen = false;

    private Vector3 startPos;    
    private Quaternion startRot; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position; 
        startRot = transform.rotation;
    }

    void Update()
    {
        if (!isFallen)
        {
            if (Vector3.Angle(Vector3.up, transform.up) > 35f)
            {
                isFallen = true;
                ScoreManager.Instance.AddPinDown();
            }
        }
    }

    public void HideFallenPin()
    {
        if (isFallen) gameObject.SetActive(false); 
    }

    public void ResetPin()
    {
        gameObject.SetActive(true);
        isFallen = false;    

        transform.position = startPos;
        transform.rotation = startRot; 

        rb.velocity = Vector3.zero;     
        rb.angularVelocity = Vector3.zero;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVusualizer : MonoBehaviour
{
    private Rigidbody rb;
    private Transform _visualTrm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _visualTrm = transform.Find("Visual");
    }

    private float beforeDirection;

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.identity;
        if (rb.velocity.x > 0.5f)//¿ì
        {
            targetRotation = Quaternion.Euler(0, 0, -30f);
        }
        else if(rb.velocity.x > -0.5f)
        {
            targetRotation = Quaternion.Euler(0, 0, +30f);
        }

        float rotateSpeed = 5f;
        float rotateAmount = Time.fixedDeltaTime * rotateSpeed;
        float current = Mathf.Sign(rb.velocity.x);


        if (Mathf.Abs(beforeDirection + current) < 0.5f)
        {
            rotateSpeed *= 5;
        }

        beforeDirection = Mathf.Sign(rb.velocity.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateAmount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 center;

    public Vector3 view;


    private void Update()
    {
        center = Camera.main.ViewportToWorldPoint(view);
    }
    private void Start()
    {
        center = Camera.main.ViewportToWorldPoint(view);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(center, 1f);
    }
}

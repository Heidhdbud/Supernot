using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trailRenderer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
    }
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        trailRenderer.Clear();
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}

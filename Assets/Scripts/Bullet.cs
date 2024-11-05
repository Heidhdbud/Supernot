using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trailRenderer;
    public bool isPlayer = false;
    AudioSource audioSource;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = PlayerController.Instance.GetComponent<AudioSource>();
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
    }
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        trailRenderer.Clear();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && isPlayer)
        {
            var enemy = collision.gameObject;
            GameManager.Instance.enemies.Remove(enemy);

            if (GameManager.Instance.enemies.Count == 0)
            {
                GameManager.Instance.EndGame(true);
            }

            enemy.SetActive(false);
            audioSource.Play();
        }
        else if (collision.gameObject.tag == "Player" && !isPlayer)
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.EndGame(false);
            audioSource.Play();
        }

        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private float speed = 2000.0f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        rigidbody.velocity = velocity * speed * Time.deltaTime;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Block"))
        {
            velocity = Vector3.Reflect(velocity, other.contacts[0].normal).normalized;
        }
    }
}

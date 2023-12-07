using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private float speed = 1500.0f;
    private Vector3 velocity = Vector3.zero;
    private bool isMoving = false;

    void Update()
    {
        rigidbody.velocity = velocity * speed * Time.deltaTime;

        if (transform.position.y < -5.0f)
        {
            velocity = Vector3.zero;
            isMoving = false;
        }
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
        isMoving = true;
    }

    public void Aggregate(Vector3 position)
    {
        Vector3 distance = position - transform.position;
        Vector3 direction = distance.normalized;
        Mathf.Abs(distance.x) / Mathf.Abs(distance.y);

    }

    public bool GetIsMove() { return isMoving; }
    public void SetIsMove(bool newIsMove) { isMoving = newIsMove; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Block"))
        {
            velocity = Vector3.Reflect(velocity, other.contacts[0].normal).normalized;
        }
    }
}

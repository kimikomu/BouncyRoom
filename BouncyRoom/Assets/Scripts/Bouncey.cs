using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bouncey : MonoBehaviour {

    [SerializeField]
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector3 initialVelocity;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    private Rigidbody ballRigidbody;
    private GameObject ball;

    private void OnEnable()
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    private void OnMouseUp()
    {

        ballRigidbody.isKinematic = false;
        ballRigidbody.velocity = initialVelocity;
    }

    void Update()
    {
        if (ballRigidbody.isKinematic)
        {
            lastFrameVelocity = ball.transform.position;
        } else
        {
            lastFrameVelocity = ballRigidbody.velocity;

        }

        Debug.Log("lastFrameVelocity On Update: " + lastFrameVelocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        ballRigidbody.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}

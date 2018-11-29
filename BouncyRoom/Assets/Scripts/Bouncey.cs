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
    private Vector3 mouse;

    public GameObject tempParent;
    public Transform grabber;


    

    void OnEnable()
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        ballRigidbody = ball.GetComponent<Rigidbody>();

    }

    private void OnMouseDown()
    {
        ballRigidbody.useGravity = false;
        ballRigidbody.isKinematic = true;
        ball.transform.position = grabber.transform.position;
        ball.transform.rotation = grabber.transform.rotation;
        ball.transform.parent = tempParent.transform;
    }

    void OnMouseUp()
    {
        ballRigidbody.useGravity = true;
        ballRigidbody.isKinematic = false;
        ballRigidbody.velocity = initialVelocity;
        ball.transform.parent = null;
        ball.transform.position = grabber.transform.position;
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

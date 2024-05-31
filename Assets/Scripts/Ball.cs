using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        InitPosition();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -1f)
        {
            InitPosition();
        }
    }

    public void InitPosition()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 2.0f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            myRigidbody.AddForce((transform.position - collision.transform.position) * 0.5f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Goal Net Red":
                GameManager.instance.AddScore("Blue");
                InitPosition();
                break;
            case "Goal Net Blue":
                GameManager.instance.AddScore("Red");
                InitPosition();
                break;
            default:
                break;
        }
    }
}

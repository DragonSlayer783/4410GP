using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxSpeed;
    public float accel;
    [SerializeField]
    private double currentSpeed;
    public float price;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        if(currentSpeed < maxSpeed)
        {
            rb.AddForce(Vector2.left * accel * Time.deltaTime);
        }

        if(gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}

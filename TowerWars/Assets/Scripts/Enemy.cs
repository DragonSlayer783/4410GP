using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController myMaster;//The AI Controller
    private Rigidbody2D rb;         //The Enemy's RigidBody
    public float baseSpeed;         //The baseSpeed of the enemy
    public float accel;             //How quickly the enemy 
    [SerializeField]
    private float maxSpeed;         //The Maximum speed the enemy can go
    [SerializeField]
    private double currentSpeed;    //The current speed the enemy is moving
    public float price;             //How much the enemy costs
    public string enemyName;        //The string name of the enemy
    
    //Start is called before the first frame update
    void Start()
    {
        //Connects to rigid body
        rb = GetComponent<Rigidbody2D>();

        //Connect itself to the AI controller
        myMaster = GameObject.Find("AIManager").GetComponent<EnemyController>();

        //Sets it's max speed to it's base + variation
        maxSpeed = baseSpeed + Random.Range(-myMaster.variation, myMaster.variation);
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy has not reached it's max speed, speed up by accelleration.
        currentSpeed = rb.velocity.magnitude;
        if(currentSpeed < maxSpeed)
        {
            rb.AddForce(Vector2.left * accel * Time.deltaTime);
        }

        //if enemy falls below map, it will tell it's parent to destroy it.
        if(gameObject.transform.position.y < -5)
        {
            myMaster.DESTROY_ME(gameObject);
        }
    }
}

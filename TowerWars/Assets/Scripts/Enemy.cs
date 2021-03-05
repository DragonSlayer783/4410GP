using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController myMaster;
    private Rigidbody2D rb;
    public float baseSpeed;
    [SerializeField]
    float maxSpeed;
    public float accel;
    [SerializeField]
    private double currentSpeed;
    public float price;
    public string enemyName;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myMaster = GameObject.Find("AIManager").GetComponent<EnemyController>();
        maxSpeed = baseSpeed + Random.Range(-myMaster.variation, myMaster.variation);
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
            myMaster.DESTROY_ME(gameObject);
        }
    }

    public float Get_Price()
    {
        return price;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameController Master;
    public GameObject SimpleEnemy;
    public Transform SpawnPoint;
    public bool ready;

    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Master.canEnemySpawn && ready)
        {
            ready = false;
            Invoke("Spawn_Simple", 1f);
        }
    }

    public void Spawn_Simple()
    {
        Instantiate(SimpleEnemy, SpawnPoint.position, Quaternion.identity);
        ready = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private GameController master;
    public List<GameObject> unitOptions;
    public List<int> counts;
    public Transform spawnPoint;
    public bool ready;
    [SerializeField]
    private float money;
    private bool gaveMoney;
    public float moneyTime;
    public float spawnTime;
    public Text moneyText;
    public int chooseIndex;
    public float waitTime;
    public bool isSpawning;
    public float variation = 0.5f;

    void Start()
    {
        //finds the game manager of the scene
        master = GameObject.Find("GameManager").GetComponent<GameController>();

        //sorts the AI's options by their price, and initializes counts size by number of units.
        unitOptions.Sort(Sort_Enemy_By_Price);
        for(int i = 0; i < unitOptions.Count; i++)
        {
            counts.Add(0);
        }

        //initializes AI manager's semaphores
        ready = false;
        gaveMoney = false;
        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //update AI's money
        moneyText.text = "AI Money: " + money.ToString();

        //if the AI has not given itself money yet, invoke add money
        if(!gaveMoney)
        {
            gaveMoney = true;
            Invoke("AddMoney", moneyTime);
        }

        //If the AI is ready and allowed to spawn a unit, spawn a unit.
        if(master.canEnemySpawn)
        {
            if(!ready)
            {
                Debug.Log("I'm not ready.  Choosing index...");
                Choose_Unit();
                Debug.Log("I have chosen unit " + chooseIndex.ToString() + ", I am ready to spawn.");
                ready = true;
            }
            else if(!isSpawning)
            {
                isSpawning = true;
                Invoke("Spawn_Unit", spawnTime);
            }
        }
    }

    //adds money to the AI's balance
    void AddMoney()
    {
        money++;

        //Ready to give money again
        gaveMoney = false;
    }
    
    //A comparison function to compare two enemy prefabs by price
    int Sort_Enemy_By_Price(GameObject e1, GameObject e2)
    {
        return e1.GetComponent<Enemy>().price.CompareTo(e2.GetComponent<Enemy>().price);
    }
    
    //will decide which unit it wants to spawn in by index
    public void Choose_Unit()
    {
        Debug.Log("Choosing...");
        chooseIndex = Random.Range(0, unitOptions.Count);
    }

    public void Spawn_Unit()
    {
        Debug.Log("Attempting to Spawn Unit.  It costs " + unitOptions[chooseIndex].GetComponent<Enemy>().price);
        //Debug.Log("I want to spawn a unit who's price is " + unitOptions[index].GetComponent<Enemy>().price.ToString());
        if(money >= unitOptions[chooseIndex].GetComponent<Enemy>().price)
        {
            Debug.Log("I have enough money, spawning...");
            money -= unitOptions[chooseIndex].GetComponent<Enemy>().price;
            Instantiate(unitOptions[chooseIndex], spawnPoint.position, Quaternion.identity);
            counts[chooseIndex]++;
            Debug.Log("I spawned the unit.  Waiting to Settle");
            Invoke("UnReady", waitTime);
        }else{
            Debug.Log("I dont have enough money.  Not enough money.");
            isSpawning = false;
        }
    }

    public void DESTROY_ME(GameObject deadEnemy)
    {
        //Manager will look for the enemy in it's unit options, and ajust the count in the parallel list.
        for (int i = 0; i < unitOptions.Count; i++)
        {
            if(unitOptions[i].GetComponent<Enemy>().enemyName == deadEnemy.GetComponent<Enemy>().enemyName)
            {
                counts[i]--;
                break;
            }
        }
        Destroy(deadEnemy);
    }

    public void UnReady()
    {
        ready = false;
        isSpawning = false;
    }
}

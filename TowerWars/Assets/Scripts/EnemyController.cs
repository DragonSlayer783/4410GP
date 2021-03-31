using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private GameController master;  //reference to game controller
    public List<GameObject> unitOptions;    //list of available units it can spawn
    public List<int> counts;    //counts of each unit
    public Transform spawnPoint;    //the enemy spawn point
    public Text moneyText;  //text on the screen to display enemy money
    [SerializeField]
    private float money;    //how much "money" the AI has.
    [SerializeField]
    public int chooseIndex; //AI's choice for what unit to spawn

    //editor variables
    public float variation; //slight speed variation for enemies
    public float moneyTime; //how long between currency generation
    public float spawnTime; //how long to wait before attempting to spawn units.

    //private semaphores
    private bool isSpawning;//spawn semaphore
    private bool ready;     //unit choice semaphore
    private bool gaveMoney; //money semaphore
    

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
            //if ai is not ready (has not chosen a unit)
            if(!ready)
            {
                //will choose unit, call itself ready
                Choose_Unit();
                ready = true;
            }
            else if(!isSpawning)
            {
                //if it's ready and isn't spawning, it will try and spawn.
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
        chooseIndex = Random.Range(0, unitOptions.Count);
    }

    //function will actually spawn units
    public void Spawn_Unit()
    {
        //if Ai can afford uniy, it will reduce money by price, spawn at spawn point, and unready itself
        if(money >= unitOptions[chooseIndex].GetComponent<Enemy>().price)
        {
            money -= unitOptions[chooseIndex].GetComponent<Enemy>().price;
            Instantiate(unitOptions[chooseIndex], spawnPoint.position, Quaternion.identity);
            counts[chooseIndex]++;
            UnReady();
        }else{  //if ai can't afford, will set spawning to false and try again later
            isSpawning = false;
        }
    }

    //will be called by enemy
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
        //destroys enemy
        Destroy(deadEnemy);
    }

    //unit will set AI ready to choose new unit and spawn new unit.
    public void UnReady()
    {
        ready = false;
        isSpawning = false;
    }
}

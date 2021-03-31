using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public float money; //player money
    bool gaveMoney = false; //player money semaphore    
    public Text moneyText; //Reference to money UI text
    public bool canEnemySpawn; //tells the AI manager if it can spawn

    void Update()
    {
        //If we haven't given money
        if (!gaveMoney)
        {
            //Call AddMoney after 1 second
            Invoke("AddMoney", 1f);

            //We gave money
            gaveMoney = true; 
        }
        
        //Update money text
        moneyText.text = "Money: " + money.ToString();
    }

    //Add money to the player
    void AddMoney()
    {
        money++;

        //Ready to give money again
        gaveMoney = false;
    }
}

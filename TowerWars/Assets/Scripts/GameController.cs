using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Money for the player
    public float money;
    
    //Used to control how fast we give money
    bool gaveMoney = false;
    
    //Reference to money UI text
    public Text moneyText;

    public bool canEnemySpawn;

    //Update is called once per frame
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

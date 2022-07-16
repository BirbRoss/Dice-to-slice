using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public GameObject[] enemies;
    public Button diceButton;
    

    public void endTurn()
    {
        diceButton.interactable = false;

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<MoveTowardsPlayer>().enemyTurn = true;
        }
    }

    public void playerTurn()
    {
        diceButton.interactable = true;
    }

    public void playerDied()
    {
        Debug.Log("Player is dead");
        //animation and shit

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

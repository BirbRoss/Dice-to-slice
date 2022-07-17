using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject[] spawners;
    public Button diceButton;
    public int moveTrack = 1;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Application.Quit();
        }
    }

    public void endTurn()
    {

        diceButton.interactable = false;

        foreach (GameObject g in enemies)
        {
            g.GetComponent<MoveTowardsPlayer>().enemyTurn = true;
        }

        foreach (GameObject g in spawners)
        {
            g.GetComponent<spawner>().spawn();
        }

        playerTurn();
    }

    public void playerTurn()
    {
        diceButton.interactable = true;
        moveTrack = 1;

        foreach (GameObject g in enemies)
        {
            g.GetComponent<MoveTowardsPlayer>().moved = false;
        }
    }

    public void playerDied()
    {
        Debug.Log("Player is dead");
        //animation and shit

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void checkEndLevel()
    {
        int enemiesDisabled = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                enemiesDisabled++;
                if (enemiesDisabled == enemies.Length)
                {
                    Debug.Log("Level is over");
                    if (SceneManager.GetActiveScene().buildIndex != 3)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        //Load next scene
                    }
                    else
                    {
                        SceneManager.LoadScene(0);
                        //Loads main menu
                    }

                }
            }
        }
    }

    public void incrementMove()
    {
        if (moveTrack <= enemies.Length)
        {
            moveTrack++;
        }
    }
}

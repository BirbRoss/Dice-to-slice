using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takeDamage : MonoBehaviour
{
    public float health;
    public Image[] hearts;
    gameManager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
    }

    public void subDamage()
    {
        health--;
        for (int i = 0; i < hearts.Length; i++)
        {
            Image temp = hearts[i];
            if (hearts[i].enabled)
            {
                hearts[i].enabled = false;
                i = hearts.Length;
            }
        }

        if (health <= 0 && gameObject.name == "Player")
        {
            manager.playerDied();
            //death
        }
        else if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

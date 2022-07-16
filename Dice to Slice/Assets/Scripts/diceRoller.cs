using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class diceRoller : MonoBehaviour
{
    public int diceRoll = 0;
    public TMP_Text rollText;
    public gameManager manager;
    public Button rollButton;

    public void roll(int diceSize)
    {
        diceRoll = Random.Range(1, diceSize+1);
        rollText.text = diceRoll.ToString();
        rollButton.interactable = false;
    }

    public void subtractPoint(int toSub)
    {
        diceRoll -= toSub;
        rollText.text = diceRoll.ToString();
    }

    public void updateUI()
    {
        rollText.text = diceRoll.ToString();

        if (diceRoll <= 0)
        {
            manager.endTurn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandle : MonoBehaviour
{
    public GameObject depressedButton;
    public GameObject extendedButton;
    public bool buttonOn;

    private void OnTriggerEnter(Collider col)
    {
        extendedButton.SetActive(false);
        depressedButton.SetActive(true);
        buttonOn = true;
    }

    public void resetButton() //Resets button to normal extended state
    {
        extendedButton.SetActive(true);
        depressedButton.SetActive(false);
        buttonOn = false;
    }
}

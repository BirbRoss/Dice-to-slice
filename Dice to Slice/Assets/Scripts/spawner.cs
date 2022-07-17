using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] int delay = 3;
    [SerializeField] int delayBetween = 3;
    int spawnPointer = 0;
    public GameObject[] toSpawn;

    public Transform batSpawn;
    public Transform skeleSpawn;
    public Transform slimeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in toSpawn)
        {
            g.transform.SetParent(transform);
            g.GetComponent<MoveTowardsPlayer>().enabled = false;
            g.SetActive(false);
        }
    }
    
    public void spawn()
    {
        if (delay == 0 && spawnPointer < toSpawn.Length)
        {
            if (toSpawn[spawnPointer].name.ToLower().Contains("bat"))
            {
                toSpawn[spawnPointer].transform.position = batSpawn.position;
                toSpawn[spawnPointer].GetComponent<MoveTowardsPlayer>().enabled = true;
                toSpawn[spawnPointer].SetActive(true);

                toSpawn[spawnPointer].transform.parent = null;
            }
            else if (toSpawn[spawnPointer].name.ToLower().Contains("skele"))
            {
                toSpawn[spawnPointer].transform.position = skeleSpawn.position;
                toSpawn[spawnPointer].GetComponent<MoveTowardsPlayer>().enabled = true;
                toSpawn[spawnPointer].SetActive(true);

                toSpawn[spawnPointer].transform.parent = null;
            }
            else if (toSpawn[spawnPointer].name.ToLower().Contains("slime"))
            {
                toSpawn[spawnPointer].transform.position = slimeSpawn.position;
                toSpawn[spawnPointer].GetComponent<MoveTowardsPlayer>().enabled = true;
                toSpawn[spawnPointer].SetActive(true);

                toSpawn[spawnPointer].transform.parent = null;
            }

            delay = delayBetween;
            spawnPointer++;

            gameObject.GetComponent<takeDamage>().subDamage();
        }
        else if (spawnPointer > toSpawn.Length)
        {
            //Should be dead but just in case
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }
        else
        {
            delay--;
        }
    }
}

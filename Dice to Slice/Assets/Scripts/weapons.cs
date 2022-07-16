using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapons : MonoBehaviour
{
    [SerializeField] bool buttonSelected = false;
    public diceRoller roller;
    public Button[] buttonList;
    public GameObject[] weapon;
    int selectedWeap;
    Ray ray;
    bool swapDone;
    public Transform Player;
    public float attackDist = 1.0f;

    [SerializeField] LayerMask ignoreLayer = 3;

    public float aoeRange = 2.0f;

    public Camera cam;
    RaycastHit hit;
    [SerializeField] GameObject selectedEnemy;
    [SerializeField] Renderer selectedRenderer;
    [SerializeField] Color defaultColor;

    void Update()
    {
        if (selectedRenderer != null)
        {
            selectedRenderer.material.color = defaultColor;
            selectedRenderer = null;
        }

        if (buttonSelected && !swapDone)
        {
            for (int i = 0; i < buttonList.Length; i++)
            {
                buttonList[i].interactable = false;
            }
            swapDone = true;
        }

        if (buttonSelected && roller.diceRoll != 0)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) //Raycast is done here to prevent it triggering every update as a part of the if
            {
                if (hit.transform.root.gameObject.tag == "Enemy")
                {
                    selectedEnemy = hit.transform.root.gameObject;
                    selectedRenderer = selectedEnemy.GetComponent<Renderer>();
                    defaultColor = selectedRenderer.material.color;

                    switch(selectedWeap)
                    {
                        case 0: //sword
                            if (Vector3.Distance(Player.position, selectedEnemy.transform.position) <= attackDist) //If the enemyy is close enough
                            {
                                selectedRenderer.material.color = Color.green;
                                if (Input.GetMouseButtonDown(0))
                                {
                                    roller.diceRoll--;
                                    roller.updateUI();
                                    selectedEnemy.gameObject.GetComponent<takeDamage>().subDamage();

                                    buttonSelected = false;
                                }
                            }
                            else
                            {
                                //Out of range
                                selectedRenderer.material.color = Color.red;
                            }
                            break;
                        case 1: //bow
                            if ((selectedEnemy.transform.position.x == Player.position.x || selectedEnemy.transform.position.z == Player.position.z)) //If the enemy is on the same cardinal direction as the player
                            {
                                selectedRenderer.material.color = Color.green;
                                if (Input.GetMouseButtonDown(0))
                                {
                                    roller.diceRoll--;
                                    roller.updateUI();
                                    selectedEnemy.gameObject.GetComponent<takeDamage>().subDamage();

                                    buttonSelected = false;
                                }
                            }
                            else 
                            {
                                //Not in line
                                selectedRenderer.material.color = Color.red;
                            }
                            break;
                        case 2: // magic
                            if (roller.diceRoll > 1)
                            {
                                selectedRenderer.material.color = Color.green;
                                if (Input.GetMouseButtonDown(0))
                                {
                                    roller.diceRoll -= 2;
                                    roller.updateUI();

                                    Collider[] enemyHits = Physics.OverlapSphere(selectedEnemy.transform.position, aoeRange);

                                    int x = 0;
                                    
                                    foreach (Collider c in enemyHits)
                                    {
                                        if (enemyHits[x].gameObject.layer != ignoreLayer)
                                        {
                                            enemyHits[x].gameObject.GetComponent<takeDamage>().subDamage();
                                        }
                                        x++;
                                    }

                                    buttonSelected = false;
                                }
                            }
                            else if (roller.diceRoll <= 1)
                            {
                                selectedRenderer.material.color = Color.red;
                                //Not enough points
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            buttonSelected = false;
            resetButtons();

            //Please roll first
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resetButtons();
        }
    }

    public void sword()
    {
        selectedWeap = 0;

        buttonSelected = true;
        weapon[0].SetActive(true);
        swapDone = false;
    }

    public void bow()
    {
        selectedWeap = 1;

        buttonSelected = true;
        weapon[1].SetActive(true);
        swapDone = false;
    }

    public void magic()
    {
        selectedWeap = 2;

        buttonSelected = true;
        weapon[2].SetActive(true);
        swapDone = false;
    }

    void resetButtons()
    {
        buttonSelected = false;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].interactable = true;
            weapon[i].SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (selectedEnemy != null)
        {
            Gizmos.DrawWireSphere(selectedEnemy.transform.position, aoeRange);
        }     
    }
}

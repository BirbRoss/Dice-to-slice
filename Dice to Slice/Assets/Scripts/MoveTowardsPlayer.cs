using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public bool enemyTurn = false;
    Transform Player;
    Vector3 movement;
    public float tilesPerMove = 1.0f;
    public float attackDist;
    public bool flying = false;
    [SerializeField] int layer = 3;
    [SerializeField] gameManager manager;
    [SerializeField] bool hasAttack;
    public int moveOrder;
    public bool moved;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        layer = LayerMask.GetMask("Ground");
        manager = GameObject.Find("GameManager").GetComponent<gameManager>();
        hasAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurn & manager.moveTrack == moveOrder && !moved)
        {
            if (flying && Vector3.Distance(transform.position, Player.position) > attackDist)
            {

                movement = Vector3.MoveTowards(transform.position, Player.position, tilesPerMove);
                movement = new Vector3((Mathf.Round(movement.x + 0.5f)) - 0.5f, transform.position.y, (Mathf.Round(movement.z + 0.5f)) - 0.5f);
                Vector3 moveDir = (transform.position - movement).normalized;
                Debug.DrawRay(transform.position, -moveDir * (tilesPerMove + 0.5f), Color.blue, 3.0f);
                if (!Physics.Raycast(transform.position, -moveDir, tilesPerMove + 0.5f))
                {
                    transform.position = movement;
                }
                //else don't move
            }
            else if (!flying && Vector3.Distance(transform.position, Player.position) > attackDist)
            {
                movement = Vector3.MoveTowards(transform.position, Player.position, tilesPerMove);
                movement = new Vector3((Mathf.Round(movement.x + 0.5f)) - 0.5f, transform.position.y, (Mathf.Round(movement.z + 0.5f)) - 0.5f);

                Vector3 moveDir = (transform.position - movement).normalized;
                Debug.DrawRay(transform.position, -moveDir, Color.blue, 3.0f);
                Debug.DrawRay(movement, Vector3.down, Color.green, 3.0f);

                if (Physics.Raycast(movement, Vector3.down, 1.0f, layer) && !Physics.Raycast(transform.position, -moveDir, 1.25f))
                {
                    transform.position = movement;
                }
                else
                {
                    float[] dirValues = new float[4];

                    if (!Physics.Raycast(transform.position, Vector3.forward, tilesPerMove) && Physics.Raycast(movement, Vector3.down, 1.25f, layer)) //Checks if anything is blocking the direction & there is ground
                    {
                        dirValues[0] = Vector3.Distance(transform.TransformDirection(Vector3.forward), Player.position); //Calculates distance to player
                        Debug.DrawRay(transform.position, Vector3.forward, Color.red, 3.0f);
                    }
                    else
                    {
                        dirValues[0] = 9999.0f; //If the raycast hits something then the value is set super high
                    }

                    if (!Physics.Raycast(transform.position, Vector3.back, tilesPerMove) && Physics.Raycast(movement, Vector3.down, 1.25f, layer))
                    {
                        dirValues[1] = Vector3.Distance(transform.TransformDirection(Vector3.back), Player.position);
                        Debug.DrawRay(transform.position, Vector3.back, Color.red, 3.0f);
                    }
                    else
                    {
                        dirValues[1] = 9999.0f;
                    }

                    if (!Physics.Raycast(transform.position, Vector3.left, tilesPerMove) && Physics.Raycast(movement, Vector3.down, 1.25f, layer))
                    {
                        dirValues[2] = Vector3.Distance(transform.TransformDirection(Vector3.left), Player.position);
                        Debug.DrawRay(transform.position, Vector3.left, Color.red, 3.0f);
                    }
                    else
                    {
                        dirValues[2] = 9999.0f;
                    }

                    if (!Physics.Raycast(transform.position, Vector3.right, tilesPerMove) && Physics.Raycast(movement, Vector3.down, 1.25f, layer))
                    {
                        dirValues[3] = Vector3.Distance(transform.TransformDirection(Vector3.right), Player.position);
                        Debug.DrawRay(transform.position, Vector3.right, Color.red, 3.0f);
                    }
                    else
                    {
                        dirValues[3] = 9999.0f;
                    }

                    float shortestID = 4;
                    float tempDist = 9999;

                    for (int i = 0; i < 4; i++)
                    {
                        if (dirValues[i] < tempDist)
                        {
                            shortestID = i;
                            tempDist = dirValues[i];
                        }
                    }

                    Debug.Log(gameObject.name + ": " + shortestID);

                    switch (shortestID)
                    {
                        case 0:
                            transform.position += Vector3.forward;
                            break;
                        case 1:
                            transform.position += Vector3.back;
                            break;
                        case 2:
                            transform.position += Vector3.left;
                            break;
                        case 3:
                            transform.position += Vector3.right;
                            break;
                        case 4: //All directions are blocked
                                //Do not move
                            break;
                    }

                }
            }
            else if (Vector3.Distance(transform.position, Player.position) <= attackDist)
            {
                if (!hasAttack) //Overengineered but some extra security never hurt
                {
                    attackPlayer();
                    hasAttack = true;
                }

            }

            moved = true;
            manager.incrementMove(); //Increments move when move is done
            enemyTurn = false;
        }
        else if (!manager.enemies[manager.moveTrack-1].activeInHierarchy || manager.enemies[manager.moveTrack - 1].GetComponent<MoveTowardsPlayer>().moved)
        {
            manager.moveTrack = moveOrder;
        }
        else
        {
            hasAttack = false;
        }
    }

    public void attackPlayer()
    {
        Player.GetComponent<takeDamage>().subDamage();
    }
}

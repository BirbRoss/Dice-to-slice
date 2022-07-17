using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] bool moved = false;
    [SerializeField] diceRoller roller;
    [SerializeField] int layer = 3;
    AudioSource walkSound;

    public Animator animator;

    private void Start()
    {
        roller = GameObject.Find("DiceManager").GetComponent<diceRoller>();
        layer = LayerMask.GetMask("Ground");
        walkSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1.0f && !moved && roller.diceRoll > 0)
        {
            if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)) * 1, 1.0f) && Physics.Raycast(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), Vector3.down, 1.0f, layer)) //Raycast done here so it isn't casting every frame
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)) * 1, Color.green);
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                moved = true;
                roller.diceRoll--;
                roller.updateUI(); //Updates UI after the movement

                animator.Play("walk");
                walkSound.PlayOneShot(walkSound.clip);
            }
        }


        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1.0f && !moved && roller.diceRoll > 0)
        {
            if(!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"))) * 1, 1.0f) && Physics.Raycast(transform.position + new Vector3(0f, 0f, Input.GetAxisRaw("Vertical")), Vector3.down, 1.0f, layer))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"))) * 1, Color.green);
                transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
                moved = true;
                roller.diceRoll--;
                roller.updateUI();

                animator.Play("walk");
                walkSound.PlayOneShot(walkSound.clip);
            }
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0.0f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0.0f)
        {
            moved = false;
        }

    }
}

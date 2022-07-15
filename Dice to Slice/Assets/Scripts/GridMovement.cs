using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] bool moved = false;

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1.0f && !moved)
        {
            if (!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)) * 1, 1.0f)) //Raycast done here so it isn't casting every frame
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)) * 1, Color.green);
                transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                moved = true;
            }
        }


        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1.0f && !moved)
        {
            if(!Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"))) * 1, 1.0f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"))) * 1, Color.green);
                transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
                moved = true;
            }
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0.0f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0.0f)
        {
            moved = false;
        }

    }
}

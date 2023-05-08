using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f; // Speed variable
    private Rigidbody rb; // Set the variable 'rb' as Rigibody

    public float dashCooldownLength = 0;
    public float dashCooldown = 0;
    public bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rb.MovePosition(transform.position + input * Time.deltaTime * speed);

        if (dashCooldown >= 0) {
            dashCooldown -= Time.deltaTime;
        }
    }
}

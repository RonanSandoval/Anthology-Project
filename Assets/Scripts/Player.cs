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
    private bool dashed = false;

    public float dashPower;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        // dash
        if (Input.GetKeyDown("space") && dashCooldown <= 0) {
            dashed = true;
            Debug.Log("dash");
            dashCooldown = dashCooldownLength;
        }

        if (dashCooldown > 0) {
            dashCooldown -= Time.deltaTime;
        }

    }


    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //rb.MovePosition(transform.position + input * Time.deltaTime * speed);
        rb.AddForce(input * speed);

        if (dashed) {
            rb.velocity = new Vector3((input * dashPower).x, rb.velocity.y, (input * dashPower).z);
            isDashing = true;
            sr.color = new Color(1,0,1,1);
            dashed = false;
        }

        if (isDashing && rb.velocity.magnitude < 15) {
            isDashing = false;
            Debug.Log("Done Dash");
            sr.color = new Color(1,1,1,1);
        }

        if (transform.position.y < -1f) {
            transform.position = new Vector3(0,2,0);
        }
    }
}

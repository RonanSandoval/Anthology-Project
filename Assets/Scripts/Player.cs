using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float speed = 1f; // Speed variable
    private Rigidbody rb; // Set the variable 'rb' as Rigibody

    public float dashCooldownLength = 0;
    public float dashCooldown = 0;
    public bool isDashing = false;
    private bool dashed = false;

    public float dashPower;
    private float dashTimer;
    public UnityEvent onDash;
    public UnityEvent onDashEnd;


    private SpriteRenderer sr;
    private bool canMove;
    private Vector3 storedDirection;

    private Vector3 spawnPoint;

    [SerializeField] private Vector3 wind;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        
        canMove = true;

        determineStartSpawnPoint();
    }

    void Update() {
        // dash
        if (canMove && Input.GetKeyDown("space") && dashCooldown <= 0) {
            dashed = true;
            dashCooldown = dashCooldownLength;
            onDash.Invoke();
        }

        if (dashCooldown > 0) {
            dashCooldown -= Time.deltaTime;
        }

        if (isDashing) {
            dashTimer += Time.deltaTime;
        } else {
            dashTimer = 0;
        }

    }


    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (input.magnitude > 0.1f) {
            storedDirection = input;
        }

        if (canMove && !isDashing) {
            //rb.MovePosition(transform.position + input * Time.deltaTime * speed);
            // rb.AddForce(input * speed);
             rb.velocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);
        }
        

        if (dashed) {
            rb.velocity = new Vector3((storedDirection * dashPower).x, 0f, (storedDirection * dashPower).z);
            isDashing = true;
            sr.color = new Color(1,0,1,1);
            dashed = false;
            rb.useGravity =false;
        }

        if (isDashing && (rb.velocity.magnitude < 15 || dashTimer > 0.3f)) {
            isDashing = false;
            sr.color = new Color(1,1,1,1);
            rb.useGravity = true;
            onDashEnd.Invoke();
        }

        // apply wind
        rb.AddForce(wind);

        if (transform.position.y < -1f) {
            respawn();
        }
    }

    public void pauseMovement() {
        canMove = false;
    }

    public void resumeMovement() {
        canMove = true;
        dashCooldown = 0.3f;
    }

    public void setSpawnPoint(Vector3 mySpawn) {
        spawnPoint = mySpawn;
    }

    private void respawn() {
        transform.position = new Vector3(0,2,0);
    }

    void determineStartSpawnPoint() {
        foreach(GameObject teleObject in GameObject.FindGameObjectsWithTag("Tele"))
        {
            if(teleObject.GetComponent<Telepoint>().index == GameSceneManager.Instance.spawnIndex)
            {
                transform.position = teleObject.transform.position;
                transform.Translate(new Vector3(0,1,0));
                break;
            }
        }
    }

    public void setWind(Vector3 windVector) {
        wind = windVector;
    }
}

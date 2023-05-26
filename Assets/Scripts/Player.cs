using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 1f; // Speed variable
    private Rigidbody rb; // Set the variable 'rb' as Rigibody
    private CapsuleCollider collider;

    public float dashCooldownLength = 0;
    public float dashCooldown = 0;
    public bool isDashing = false;
    private bool dashed = false;

    public float dashPower;
    private float dashTimer;
    public UnityEvent onDash;
    public UnityEvent onDashEnd;

    public bool isRespawning = false;


    private SpriteRenderer sr;
    [SerializeField] bool canMove;
    [SerializeField] private bool canDash;
    private Vector3 storedDirection;

    private Vector3 spawnPoint;

    [SerializeField] ParticleSystem respawnPS;

    [SerializeField] private Vector3 wind;

    SoundController sc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider>();
        sc = GetComponent<SoundController>();
        
        canMove = true;
        canDash = true;
        //canDash = GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.TalkedToDenial);

        GameSceneManager.Instance.onSceneChange.AddListener(pauseMovement);

        if (SceneManager.GetActiveScene().name == "Anger") {
                GameProgressManager.Instance.addProgress(GameProgressManager.ProgressFlag.AngerTeleport);
        }

        determineStartSpawnPoint();
    }

    void Update() {
        // dash
        if (canDash && canMove && dashKeys() && dashCooldown <= 0) {
            Debug.Log("dashed!");
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

        // flip sprite
        if (storedDirection.x < 0) {
            sr.flipX = true;
            transform.GetChild(0).gameObject.transform.localScale = new Vector3(-0.5f,1f,0.5f);
        } else if (storedDirection.x > 0) {
            sr.flipX = false;
            transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.5f,1f,0.5f);
        }

    }

    bool dashKeys() {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z);
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
            rb.useGravity = false;
            sc.playRandomizedSound();
        }

        if (isDashing && (rb.velocity.magnitude < 15 || dashTimer > 0.3f)) {
            isDashing = false;
            sr.color = new Color(1,1,1,1);
            rb.useGravity = true;
            onDashEnd.Invoke();
        }

        // apply wind
        if(canMove) {
            rb.AddForce(wind);
        }

        // check if fallen
        if (transform.position.y < -1f && !isRespawning) {
            isRespawning = true;
            collider.enabled = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            pauseMovement();
            StartCoroutine(respawn());
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
        spawnPoint.Set(spawnPoint.x, spawnPoint.y + 1, spawnPoint.z);
    }

    public Vector3 getSpawnPoint() {
        return spawnPoint;
    }

    public void setDash(bool myDash) {
        canDash = myDash;
    }

    IEnumerator respawn() {
        respawnPS.Play();
        sc.playSound(0);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(transform.position, spawnPoint) > 0.5f) {
            transform.position = Vector3.Lerp(transform.position, spawnPoint, Time.deltaTime * 7);
            yield return null;
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        sc.playSound(1);
        transform.position = spawnPoint;
        isRespawning = false;
        collider.enabled = true;
        rb.useGravity = true;
        resumeMovement();
        respawnPS.Play();
       
    }

    void determineStartSpawnPoint() {
        foreach(GameObject teleObject in GameObject.FindGameObjectsWithTag("Tele"))
        {
            try {
                if(teleObject.GetComponent<Telepoint>().index == GameSceneManager.Instance.spawnIndex)
                {
                    transform.position = teleObject.transform.position;
                    transform.Translate(new Vector3(0,1,0));
                    return;
                }
            } catch {}
        }
        
        transform.position = new Vector3(0,2,0);
        spawnPoint = new Vector3(0,2,0);
    }

    public void setWind(Vector3 windVector) {
        wind = windVector;
    }

    public void forceDash(Vector3 dashDirection) {
        storedDirection = dashDirection.normalized;
        dashCooldown = dashCooldownLength;
        onDash.Invoke();
        rb.velocity = new Vector3((storedDirection * dashPower).x, 0f, (storedDirection * dashPower).z);
        isDashing = true;
        sr.color = new Color(1,0,1,1);
        rb.useGravity = false;
        sc.playRandomizedSound();
    }

    public IEnumerator teleport() {
        canMove = false;

        Vector3 floatPoint = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        while (Vector3.Distance(transform.position, floatPoint) > 0.5f) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * 2);
            transform.position = Vector3.Lerp(transform.position, floatPoint, Time.deltaTime * 4);
            yield return null;
        }

    }
}

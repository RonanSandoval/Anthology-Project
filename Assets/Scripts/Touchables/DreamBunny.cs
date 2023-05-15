using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBunny : Touchable
{

    private GameObject player;

    [SerializeField] float distanceThreshold;
    [SerializeField] float speed;
    [SerializeField] float returnWaitTime;
    [SerializeField] float moveWaitTime;
    [SerializeField] float homeRadius;
    private Vector3 homePoint;
    private Vector3 movePoint;

    private Rigidbody rb;
    [SerializeField] BoxCollider collisionBox;

    private float waitCounter;

    enum State {Idle, Running, Waiting, Returning}
    State currentState;

    protected override void onTouch()
    {
        GameTaskManager.Instance.updateTask(0, 1);
        Destroy(gameObject);
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        homePoint = transform.position;
        currentState = State.Idle;
        movePoint = homePoint;
    }

    void FixedUpdate()
    {
        if(currentState == State.Running) {
            Vector3 runVector = transform.position - player.transform.position;
            rb.velocity = runVector.normalized * speed;
            waitCounter = returnWaitTime;
            collisionBox.enabled = true;
        } else if (currentState == State.Returning) {
            Vector3 runVector = homePoint - transform.position;
            rb.velocity = runVector.normalized * speed;
            collisionBox.enabled = false;
        } else if (currentState == State.Idle) {
            if (waitCounter <= 0) {
                waitCounter = moveWaitTime;
                movePoint = homePoint + (Random.insideUnitSphere * homeRadius);
                movePoint.y = homePoint.y;
            }
            if (Vector3.Distance(transform.position, movePoint) - 1 > 1f) {
                 Vector3 runVector = movePoint - transform.position;
                rb.velocity = runVector.normalized * speed;
            }
        }
    }

    void Update() {
        waitCounter -= Time.deltaTime;
        determineState();
    }

    void determineState() {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceThreshold) {
            currentState = State.Running;
        } else if (currentState == State.Running && Vector3.Distance(transform.position, player.transform.position) >= distanceThreshold) {
            currentState = State.Waiting;
        } else if (currentState == State.Waiting && waitCounter <= 0) {
            currentState = State.Returning;
        } else if (currentState == State.Returning && Vector3.Distance(transform.position, homePoint) - 1 < homeRadius) {
            currentState = State.Idle;
        }
    }
}

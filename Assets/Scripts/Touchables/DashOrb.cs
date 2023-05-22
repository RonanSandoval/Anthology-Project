using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb : Touchable
{
    bool ready;

    enum direction {up, down, left, right};

    GameObject pointer;

    [SerializeField] direction dashDirection;
    Vector3 directionVector;

    // Start is called before the first frame update
    void Start()
    {
        pointer = transform.GetChild(0).gameObject;
        ready = true;

        if (dashDirection == direction.up) {
            directionVector = Vector3.forward;
            pointer.transform.Rotate(new Vector3(0,0,180));
        } else if (dashDirection == direction.down) {
            directionVector = Vector3.back;
        } else if (dashDirection == direction.left) {
            directionVector = Vector3.left;
            pointer.transform.Rotate(new Vector3(0,0,270));
        } else {
            directionVector = Vector3.right;
            pointer.transform.Rotate(new Vector3(0,0,90));
        }
    }

    protected override void onTouch()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().forceDash(directionVector);
        ready = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : Touchable
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    protected override void onTouch()
    {
        if (player.getSpawnPoint() != transform.position) {
            player.setSpawnPoint(transform.position);
            Debug.Log("spawn set to" + transform.position);
        }
    }
}

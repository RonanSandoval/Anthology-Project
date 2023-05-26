using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : Touchable
{
    Player player;
    SoundController sc;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sc = GetComponent<SoundController>();
        ps = GetComponentInChildren<ParticleSystem>();
    }


    protected override void onTouch()
    {
        if (!player.getSpawnPoint().Equals(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z))) {
            player.setSpawnPoint(transform.position);
            Debug.Log("spawn set to" + transform.position);
            sc.playSound(0);
            ps.Play();
        }
    }
}

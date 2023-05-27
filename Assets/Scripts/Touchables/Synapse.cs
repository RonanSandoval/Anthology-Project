using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Synapse : Touchable
{
    Player player;
    SoundController sc;
    [SerializeField] ParticleSystem ps;
    [SerializeField] ParticleSystem psConstant;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sc = GetComponent<SoundController>();
    }

    void Update() {
        if (player.getSpawnPoint().Equals(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z))) {
             psConstant.Play();
        } else {
            psConstant.Stop();
        }
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

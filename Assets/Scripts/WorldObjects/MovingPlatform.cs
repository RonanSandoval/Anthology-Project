using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject target=null;
    private Vector3 offset;
    private ParticleSystem ps;

    void Start(){
        target = null;
        ps = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.scale = new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = Mathf.Max(transform.localScale.x * transform.localScale.y * transform.localScale.z / 3, 10);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().onRespawn.AddListener(unhook);
    }
    void OnTriggerStay(Collider col){
        
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit(Collider col){
        target = null;
    }

    void LateUpdate(){
        if (target != null) {
            Debug.Log("touching");
            target.transform.position = transform.position+offset;
        }
    }

    void unhook() {
        target = null;
    }
}

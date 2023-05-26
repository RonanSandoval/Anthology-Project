using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : Interactable
{
    // Scene that this telepoint will go to
    public string toScene;
    public int toIndex;
    public int index;
    [SerializeField] GameProgressManager.ProgressFlag accessFlag;
    bool accessible;
    SoundController sc;

    SpriteRenderer sr;
    ParticleSystem ps;

    protected override void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();

        accessible = GameProgressManager.Instance.checkProgress(accessFlag);
        sr.color = accessible ? new Color(1,1,1,0.5f) : new Color(1,1,1,0.1f);
        GameProgressManager.Instance.onAddProgress.AddListener(checkAccess);
        sc = GetComponent<SoundController>();
        ps = GetComponentInChildren<ParticleSystem>();

        base.Start();
    }

    protected override void onInteract() {
        if (accessible) {
            sc.playSound(1);
            ps.Play();
            GameSceneManager.Instance.setSpawnIndex(toIndex);
            StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().teleport());
            StartCoroutine(teleport());
        }
    }

    IEnumerator teleport() {
        yield return new WaitForSeconds(1.5f);
        GameSceneManager.Instance.changeSceneFade(toScene);
    }

    void checkAccess() {
        accessible = GameProgressManager.Instance.checkProgress(accessFlag);
        sr.color = accessible ? new Color(1,1,1,0.5f) : new Color(1,1,1,0.1f);
    }
}

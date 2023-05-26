using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] List<AudioClip> soundList;
    [SerializeField] List<AudioClip> randomizedSounds;

    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void playSound(int soundIndex) {
        audioSource.Stop();
        audioSource.PlayOneShot(soundList[soundIndex]);
    }

    public void playSoundWorldly(int soundIndex) {
        AudioSource.PlayClipAtPoint(soundList[soundIndex], GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public void playRandomizedSound() {
        audioSource.PlayOneShot(randomizedSounds[Random.Range(0,randomizedSounds.Count)]);
    }
}

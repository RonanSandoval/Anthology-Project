using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class plsgod : MonoBehaviour
{

    public AudioSource music; // drag the audio source here
    public AudioClip notakeywordthistime;
    public AudioClip denial;
    public AudioClip ab;
    public AudioClip ac;
    public AudioClip ad;
    public AudioClip abc;
    public AudioClip abd;
    public AudioClip acd;
    public AudioClip abcd;
    // .. (all possible variations), drag the appropriate clips in place

    // bools that say if we've completed an area or not
   

    // Start is called before the first frame update
    void Start()
    {
        bool a = GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.FinishedDenial);
        bool d = GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.FinishedAnger);
        bool b = GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.FinishedBargaining);
        bool c = GameProgressManager.Instance.checkProgress(GameProgressManager.ProgressFlag.FinishedDepression);

        if (a){
        music.clip = denial;
        }

        else if (a && b){
            music.clip = ab;
        }

        else if (a && c){
            music.clip = ac;
        }

        else if (a && d){
            music.clip = ad;
        }

        else if (a && b && c){
            music.clip = abc;
        }

        else if (a && b && d){
            music.clip = abd;
        }

        else if (a && c && d){
            music.clip = acd;
        }

        else if (a && b && c && d){
            music.clip = abcd;
        }

        else {
            music.clip = notakeywordthistime;
        }

        if (!music.isPlaying){
            music.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

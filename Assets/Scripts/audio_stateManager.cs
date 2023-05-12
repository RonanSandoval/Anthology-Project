using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audio_stateManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the audio mixer
    public string busName; // Name of the audio bus to apply the filter to
    public float low_startCutoffFrequency = 20000f; // Starting cutoff frequency of the lowpass filter
    public float low_endCutoffFrequency = 500f; // Ending cutoff frequency of the lowpass filter
    public float high_startCutoffFrequency = 10f; //Hipass start
    public float high_endCutoffFrequency = 250f; //Hipass end
    public float volume_start = 0f;
    public float volume_end = -6f;
    public float transitionTime = 1f; // Time it takes for the filter to transition from start to end frequency

    private float low_currentCutoffFrequency; // Current cutoff frequency of the lowpass filter
    private float high_currentCutoffFrequency;
    private float volume_current;
    private float timeElapsed_out; // Time elapsed since the filter started transitioning
    private float timeElapsed_in;

    private void Start()
    {
        // Set the starting cutoff frequency of the lowpass filter
        audioMixer.SetFloat(busName + ".Lowpass", low_startCutoffFrequency);
        low_currentCutoffFrequency = low_startCutoffFrequency;

        audioMixer.SetFloat(busName + ".Highpass", high_startCutoffFrequency);
        high_currentCutoffFrequency = high_startCutoffFrequency; 
        
        audioMixer.SetFloat(busName + ".Volume", volume_start);
        volume_current = volume_start;      
    }

    void Update()
    {
        if (GameStateManager.Instance.checkState(GameStateManager.GameState.Talking))
        {
            if (low_currentCutoffFrequency > low_endCutoffFrequency)
            {
            // Calculate the new cutoff frequency based on the transition time
            timeElapsed_out += Time.deltaTime;
            low_currentCutoffFrequency = Mathf.Lerp(low_startCutoffFrequency, low_endCutoffFrequency, timeElapsed_out / transitionTime);
            high_currentCutoffFrequency = Mathf.Lerp(high_startCutoffFrequency, high_endCutoffFrequency, timeElapsed_out / transitionTime);
            volume_current = Mathf.Lerp(volume_start, volume_end, timeElapsed_out / transitionTime);

            // Set the new cutoff frequency of the lowpass filter
            audioMixer.SetFloat(busName + ".Lowpass", low_currentCutoffFrequency);
            audioMixer.SetFloat(busName + ".Highpass", high_currentCutoffFrequency);
            audioMixer.SetFloat(busName + ".Volume", volume_current);
            }
            else
            {
            timeElapsed_out = 0f;
            }
        }
        else 
        {
            if (low_currentCutoffFrequency < low_startCutoffFrequency)
            {
            // Calculate the new cutoff frequency based on the transition time
            timeElapsed_in += Time.deltaTime;
            low_currentCutoffFrequency = Mathf.Lerp(low_endCutoffFrequency, low_startCutoffFrequency, timeElapsed_in / transitionTime);
            high_currentCutoffFrequency = Mathf.Lerp(high_endCutoffFrequency, high_startCutoffFrequency, timeElapsed_in / transitionTime);
            volume_current = Mathf.Lerp(volume_end, volume_start, timeElapsed_in / transitionTime);

            // Set the new cutoff frequency of the lowpass filter
            audioMixer.SetFloat(busName + ".Lowpass", low_currentCutoffFrequency);
            audioMixer.SetFloat(busName + ".Highpass", high_currentCutoffFrequency);
            audioMixer.SetFloat(busName + ".Volume", volume_current);
            }
            else
            {
            timeElapsed_in = 0f;
            }
        }
    }
}

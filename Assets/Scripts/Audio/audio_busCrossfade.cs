using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audio_busCrossfade : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the audio mixer
    public string busNameFadeOut; // Name of the audio bus to fade out
    public string busNameFadeIn; // Name of the audio bus to fade in
    public float fadeTime = 5f; // Time in seconds to fade out the audio

    private bool isFading = false; // Flag indicating whether the audio is currently being faded
    private float initialVolumeFadeOut; // Initial volume of the audio bus to fade out
    private float initialVolumeFadeIn; // Initial volume of the audio bus to fade in
    private float currentFadeTime; // Current time elapsed during the fade out

    private void OnTriggerEnter(Collider other)
    {
        if (!isFading && other.CompareTag("Player"))
        {
            isFading = true;

            // Get the initial volumes of the audio buses
            audioMixer.GetFloat(busNameFadeOut + ".Volume", out initialVolumeFadeOut);
            audioMixer.GetFloat(busNameFadeIn + ".Volume", out initialVolumeFadeIn);

            // Start fading out the volume of the first audio bus and fading in the volume of the second audio bus
            currentFadeTime = 0f;
            StartCoroutine(FadeOutAudio());
            StartCoroutine(FadeInAudio());
        }
    }

    private IEnumerator FadeOutAudio()
    {
        while (currentFadeTime < fadeTime + 3)
        {
            currentFadeTime += Time.deltaTime;

            // Calculate the new volume of the first audio bus using a linear interpolation
            float newVolumeFadeOut = Mathf.Lerp(initialVolumeFadeOut, -40f, currentFadeTime / fadeTime);

            // Set the volume of the first audio bus
            audioMixer.SetFloat(busNameFadeOut + ".Volume", newVolumeFadeOut);

            yield return null;
        }
    }

    private IEnumerator FadeInAudio()
    {
        float currentFadeInVolume = initialVolumeFadeIn;

        while (currentFadeTime < fadeTime)
        {
            currentFadeTime += Time.deltaTime;

            // Calculate the new volume of the second audio bus using a linear interpolation
            float newVolumeFadeIn = Mathf.Lerp(initialVolumeFadeIn, 0f, currentFadeTime / fadeTime);

            // Set the volume of the second audio bus
            audioMixer.SetFloat(busNameFadeIn + ".Volume", newVolumeFadeIn);

            // Update the current fade-in volume for the next iteration
            currentFadeInVolume = newVolumeFadeIn;

            yield return null;
        }

        // Reset the flag indicating that the audio is no longer fading
        isFading = false;
    }
}

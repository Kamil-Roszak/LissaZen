using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FrequencyGenerator : MonoBehaviour
{
    private AudioSource audioSource;
    private float[] frequencies = { 261.63f, 293.66f, 329.63f, 349.23f, 392.00f, 440.00f, 493.88f }; // C4 to B4
    private float sampleRate = 44100f;
    private float currentFrequency = 440f;
    private float time;

    void Start()
    {
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * currentFrequency * time / sampleRate);
            time += 1f;
        }
    }

    private IEnumerator ChangeFrequencyRoutine()
    {
        while (true)
        {
            currentFrequency = frequencies[Random.Range(0, frequencies.Length)]; // Change to a random note
            yield return new WaitForSeconds(0.5f); // Change note every 0.5s
        }
    }
}

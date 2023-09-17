using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSync : MonoBehaviour
{
    public float bpm;
    public int beatsPerMeasure = 4;
    public AudioSource musicSource;

    private int previousMeasure = -1;

    public int MeasureCount
    {
        get
        {
            float timePerMeasure = 60 * beatsPerMeasure / bpm;
            return Mathf.FloorToInt(musicSource.time / timePerMeasure);
        }
    }
    public float BeatInMeasure
    {
        get
        {
            float timePerMeasure = 60 * beatsPerMeasure / bpm;
            return (musicSource.time - timePerMeasure * MeasureCount) * bpm / 60;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentMeasure = MeasureCount;
        if (currentMeasure != previousMeasure)
        {
            previousMeasure = currentMeasure;
        }
    }
}

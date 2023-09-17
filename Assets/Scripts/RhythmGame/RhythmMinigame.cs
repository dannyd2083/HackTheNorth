using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static RhythmController;

public class RhythmMinigame : MonoBehaviour
{
    public float sizePerBeat = 64f;
    public float leadingBeats = 0.5f;
    public float horizontalPadding = 10f;

    public float bpm = 118f;
    public float timeMultiplier = 1f;

    public float swagThreshold = 0.04f;
    public float okayThreshold = 0.08f;
    public float badThreshold = 0.16f;

    public Color swagColor = Color.white;
    public Color okayColor = Color.yellow;
    public Color badColor = Color.red;

    public RhythmTrackPlay mimicTrack;
    public RhythmTrackPlay makerTrack;
    public Image image;

    public Sprite swagSprite;
    public Sprite okaySprite;
    public Sprite badSprite;

    public MusicSync bgm;
    public AudioSource swagSource;
    public AudioSource okaySource;
    public AudioSource badSource;

    private RhythmPattern pattern;

    public void ApplyRhythmPattern(RhythmPattern pattern)
    {
        this.pattern = pattern;
        
        mimicTrack.ApplyRhythmPattern(pattern);
        makerTrack.ApplyRhythmPattern(pattern);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalPadding + (pattern.length + leadingBeats) * sizePerBeat);
    }

    public IEnumerator PlayPattern(RhythmScore score)
    {
        float leadingBeatNum = 1f - leadingBeats;
        bool lowBeat = false;
        while (!lowBeat || bgm.BeatInMeasure - Mathf.Floor(bgm.BeatInMeasure) < leadingBeatNum)
        {
            yield return null;
            if (bgm.BeatInMeasure - Mathf.Floor(bgm.BeatInMeasure) < leadingBeatNum)
            {
                lowBeat = true;
            }
        }
        StartCoroutine(mimicTrack.PlayPattern(score));
        yield return new WaitForSeconds(pattern.length / timeMultiplier * 60 / bpm);
        StartCoroutine(makerTrack.PlayPattern(score));
        yield return new WaitForSeconds((pattern.length + leadingBeats) / timeMultiplier * 60 / bpm);
        float scoreRatio = score.CalculateSuccessRatio(pattern.keys.Count);
        if (scoreRatio >= 0.8)
        {
            image.sprite = swagSprite;
            swagSource.Play();
        }
        else if (scoreRatio >= 0.5)
        {
            image.sprite = okaySprite;
            okaySource.Play();
        }
        else
        {
            image.sprite = badSprite;
            badSource.Play();
        }
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f / timeMultiplier * 60 / bpm);
        image.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        mimicTrack.sizePerBeat = makerTrack.sizePerBeat = sizePerBeat;
        mimicTrack.leadingBeats = makerTrack.leadingBeats = leadingBeats;
        mimicTrack.horizontalPadding = makerTrack.horizontalPadding = horizontalPadding;

        mimicTrack.bpm = makerTrack.bpm = bpm;
        mimicTrack.timeMultiplier = makerTrack.timeMultiplier = timeMultiplier;
        mimicTrack.swagThreshold = makerTrack.swagThreshold = swagThreshold;
        mimicTrack.okayThreshold = makerTrack.okayThreshold = okayThreshold;
        mimicTrack.badThreshold = makerTrack.badThreshold = badThreshold;

        mimicTrack.swagColor = makerTrack.swagColor = swagColor;
        mimicTrack.okayColor = makerTrack.okayColor = okayColor;
        mimicTrack.badColor = makerTrack.badColor = badColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

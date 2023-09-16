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

    public TextAsset easyPatternsFile;
    public TextAsset mediumPatternsFile;
    public TextAsset hardPatternsFile;

    public MusicSync bgm;

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
        float leadingBeatNum = bgm.beatsPerMeasure - leadingBeats;
        bool lowBeat = false;
        while (!lowBeat || bgm.BeatInMeasure < leadingBeatNum)
        {
            yield return null;
            if (bgm.BeatInMeasure < leadingBeatNum)
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
        }
        else if (scoreRatio >= 0.5)
        {
            image.sprite = okaySprite;
        }
        else
        {
            image.sprite = badSprite;
        }
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(leadingBeats / timeMultiplier * 60 / bpm);
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

        AvailablePatterns easyPatterns = JsonUtility.FromJson<AvailablePatterns>(hardPatternsFile.text);
        RhythmPattern chosen = easyPatterns.patterns[Random.Range(0, easyPatterns.patterns.Count)];
        ApplyRhythmPattern(chosen);
        /*
        ApplyRhythmPattern(new RhythmController.RhythmPattern(
            new List<RhythmController.RhythmKey>(
                new RhythmController.RhythmKey[]
                {
                    new RhythmController.RhythmKey(0.0f, "This", MoveDirection.Left),
                    new RhythmController.RhythmKey(1.0f, "is", MoveDirection.Right),
                    new RhythmController.RhythmKey(2.0f, "a", MoveDirection.Up),
                    new RhythmController.RhythmKey(3.0f, "test", MoveDirection.Down),

                }),
            4.0f));
        */
        Debug.Log(JsonUtility.ToJson(pattern));
        RhythmScore score = new RhythmScore();
        StartCoroutine(PlayPattern(score));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

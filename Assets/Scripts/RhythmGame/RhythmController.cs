using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class RhythmController : MonoBehaviour
{
    [Serializable]
    public struct RhythmKey
    {
        public float beat;
        public string phrase;
        /*
         * Seriaization: Left = 0; Up = 1; Right = 2; Down = 3; None = 4
         */
        public MoveDirection key;

        public RhythmKey(float beat = 0f, string phrase = "", MoveDirection key = MoveDirection.None)
        {
            this.beat = beat;
            this.phrase = phrase;
            this.key = key;
        }

        public override string ToString()
        {
            return $"{phrase} (Beat {beat}; Key {key})";
        }
    }

    [Serializable]
    public struct RhythmPattern
    {
        public List<RhythmKey> keys;
        public float length;

        public RhythmPattern(List<RhythmKey> keys, float length)
        {
            this.keys = keys ?? new List<RhythmKey>();
            this.length = length;
        }
    }

    [Serializable]
    public struct AvailablePatterns
    {
        public List<RhythmPattern> patterns;

        public AvailablePatterns(List<RhythmPattern> patterns)
        {
            this.patterns = patterns;
        }
    }


    public List<RhythmPattern> patterns;
    public float bpm = 118.0f;
    public float yOffset = 288f;

    public TextAsset easyPatternsFile;
    public TextAsset mediumPatternsFile;
    public TextAsset hardPatternsFile;

    public RhythmMinigame minigame;

    private float initialYPos = 0f;
    private RectTransform rect;
    private bool isRunning = false;
    
    public IEnumerator SlideIn(float totalTime)
    {
        Debug.Log("Sliding In");
        float timePassed = 0f;
        Vector2 newPos;
        while (timePassed < totalTime)
        {
            yield return null;
            timePassed += Time.deltaTime;
            float tNorm = (totalTime - timePassed) / totalTime;
            newPos = new Vector2(rect.anchoredPosition.x, initialYPos - yOffset * tNorm * tNorm);
            rect.anchoredPosition = newPos;
        }
        newPos = new Vector2(rect.anchoredPosition.x, initialYPos);
        rect.anchoredPosition = newPos;
        Debug.Log("Sliding In Complete");
    }
    public IEnumerator SlideOut(float totalTime)
    {
        Debug.Log("Sliding Out");
        float timePassed = 0f;
        Vector2 newPos;
        while (timePassed < totalTime)
        {
            yield return null;
            timePassed += Time.deltaTime;
            float tNorm = (timePassed) / totalTime;
            newPos = new Vector2(rect.anchoredPosition.x, initialYPos - yOffset * tNorm * tNorm);
            rect.anchoredPosition = newPos;
        }
        newPos = new Vector2(rect.anchoredPosition.x, initialYPos - yOffset);
        rect.anchoredPosition = newPos;
        Debug.Log("Sliding Out Complete");
    }

    public IEnumerator RhythmSequence (int difficulty)
    {
        if (isRunning) { yield break; }
        isRunning = true;
        string text = "";
        switch (difficulty)
        {
            case 1:
                text = easyPatternsFile.text;
                break;
            case 2:
                text = mediumPatternsFile.text;
                break;
            case 3:
                text = hardPatternsFile.text;
                break;
        }

        AvailablePatterns patterns = JsonUtility.FromJson<AvailablePatterns>(text);
        RhythmPattern chosen = patterns.patterns[UnityEngine.Random.Range(0, patterns.patterns.Count)];
        minigame.ApplyRhythmPattern(chosen);

        yield return SlideIn(0.2f);

        RhythmScore score = new RhythmScore();

        yield return minigame.PlayPattern(score);

        yield return SlideOut(0.2f);
        isRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        initialYPos = rect.anchoredPosition.y;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, initialYPos - yOffset);

        StartCoroutine(RhythmSequence(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

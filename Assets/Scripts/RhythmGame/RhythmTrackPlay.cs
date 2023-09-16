using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RhythmTrackPlay : MonoBehaviour
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

    public bool isPlayback = false;

    public GameObject button;
    public GameObject bar;

    private float beats = 4.0f;
    private RectTransform rect;
    private RhythmController.RhythmPattern pattern;
    private GameObject createdBar;
    private List<GameObject> createdButtons;
    public float Beats
    {
        get { return beats; }
        set 
        { 
            beats = value;
            rect ??= GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizePerBeat * (value + leadingBeats) + 2 * horizontalPadding);
        }
    }
    public void ApplyRhythmPattern(RhythmController.RhythmPattern pattern)
    {
        this.pattern = pattern;
        Beats = pattern.length;
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        createdBar = null;
        createdButtons = new List<GameObject>();
        if (!isPlayback)
        {
            foreach (RhythmController.RhythmKey key in pattern.keys)
            {
                GameObject newObject = Instantiate(button, transform);
                RhythmButton buttonScript = newObject.GetComponent<RhythmButton>();
                buttonScript.Direction = key.key;
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(horizontalPadding + (key.beat + leadingBeats) * sizePerBeat, 0, 0);
                buttonScript.phrase.text = key.phrase;
                createdButtons.Add(newObject);
            }
        }
    }

    private MoveDirection GetPressedButton()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return MoveDirection.Up;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return MoveDirection.Left;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return MoveDirection.Down;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return MoveDirection.Right;
        }
        return MoveDirection.None;
    }

    public IEnumerator PlayPattern(RhythmScore score)
    {
        if (createdBar != null) { yield break; }
        
        createdBar = Instantiate(bar, transform);
        float timePassed = -leadingBeats * 60 / bpm;
        float CalculateBeat()
        {
            return (timePassed * bpm) / 60;
        }
        RectTransform rect = createdBar.GetComponent<RectTransform>();
        Vector3 position = new Vector3(horizontalPadding, 0);
        rect.anchoredPosition = position;

        int currentIndex = 0;
        
        while (CalculateBeat() < pattern.length)
        {
            yield return null;
            timePassed += Time.deltaTime * timeMultiplier;
            position.x = horizontalPadding + (CalculateBeat() + leadingBeats) * sizePerBeat;
            rect.anchoredPosition = position;

            if (isPlayback)
            {
                // Score calculation
                MoveDirection keyPressed = GetPressedButton();

                if (keyPressed != MoveDirection.None)
                {
                    GameObject newObject = Instantiate(button, transform);
                    RhythmButton buttonScript = newObject.GetComponent<RhythmButton>();
                    Text text = buttonScript.phrase;
                    buttonScript.Direction = keyPressed;
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(horizontalPadding + (CalculateBeat() + leadingBeats) * sizePerBeat, 0, 0);
                    text.text = "??";
                    text.color = badColor;

                    if (currentIndex < pattern.keys.Count)
                    {
                        float error = timePassed - pattern.keys[currentIndex].beat * 60 / bpm;
                        if (Math.Abs(error) <= badThreshold)
                        {
                            if (keyPressed != pattern.keys[currentIndex].key)
                            {
                                // Wrong key
                                score.badCount++;
                            }
                            else if (Math.Abs(error) <= swagThreshold)
                            {
                                score.swagCount++;
                                text.text = pattern.keys[currentIndex].phrase;
                                text.color = swagColor;
                            }
                            else if (Math.Abs(error) <= okayThreshold)
                            {
                                score.okayCount++;
                                text.text = pattern.keys[currentIndex].phrase + "?";
                                text.color = okayColor;
                            }
                            else
                            {
                                score.badCount++;
                            }
                            currentIndex++;
                        }
                    }
                }
                if (currentIndex < pattern.keys.Count && (timePassed - pattern.keys[currentIndex].beat * 60 / bpm) > badThreshold)
                {
                    score.badCount++;
                    currentIndex++;
                }
            }
        }
        if (isPlayback)
        {
            if (currentIndex < pattern.keys.Count)
            {
                score.badCount += pattern.keys.Count - currentIndex;
            }
            Debug.Log($"Swag: {score.swagCount}; Okay: {score.okayCount}; Bad: {score.badCount}");
        }
        Destroy(createdBar);
        createdBar = null;
    }
    // Start is called before the first frame update
    void Start()
    {   
        // RhythmScore score = new RhythmScore();
        // StartCoroutine(PlayPattern(score));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

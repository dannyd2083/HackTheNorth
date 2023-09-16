using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RhythmTrackPlay : MonoBehaviour
{
    public float sizePerBeat = 64f;
    public float leadingBeats = 1f;
    public float horizontalPadding = 10f;
    public GameObject button;

    private float beats = 4.0f;
    private RectTransform rect;
    public float Beats
    {
        get { return beats; }
        set 
        { 
            beats = value;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizePerBeat * (value + leadingBeats) + 2 * horizontalPadding);
        }
    }
    public void ApplyRhythmPattern(RhythmController.RhythmPattern pattern)
    {
        Beats = pattern.length;
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(RhythmController.RhythmKey key in pattern.keys)
        {
            GameObject newObject = Instantiate(button, transform);
            RhythmButton buttonScript = newObject.GetComponent<RhythmButton>();
            buttonScript.Direction = key.key;
            newObject.GetComponent<RectTransform>().anchoredPosition = new Vector3 (horizontalPadding + (key.beat + leadingBeats) * sizePerBeat, 0, 0);
            buttonScript.phrase.text = key.phrase;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

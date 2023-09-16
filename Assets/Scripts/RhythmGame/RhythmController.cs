using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RhythmController : MonoBehaviour
{
    [Serializable]
    public struct RhythmKey
    {
        public float beat;
        public string phrase;
        public MoveDirection key;

        public RhythmKey(float beat, string phrase, MoveDirection key)
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
            this.keys = keys;
            this.length = length;
        }
    }

    public List<RhythmPattern> patterns;
    public float bpm = 118.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

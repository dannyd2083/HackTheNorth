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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

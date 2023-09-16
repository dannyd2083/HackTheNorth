using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RhythmController : MonoBehaviour
{
    [Serializable]
    public struct RhythmKey {
        public float beat;
        public string phrase;
        public MoveDirection key;
    }

    [Serializable]
    public struct RhythmPattern
    {
        public List<RhythmKey> keys;
        public float length;
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

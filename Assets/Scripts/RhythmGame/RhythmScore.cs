using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScore
{
    public int swagCount = 0;
    public int okayCount = 0;
    public int badCount = 0;

    public float CalculateSuccessRatio(int keyCount)
    {
        return (float)(swagCount * 2 + okayCount) / (keyCount * 2);
    }
}

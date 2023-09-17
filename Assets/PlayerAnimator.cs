using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public float animationRate = 0.2f;
    public Sprite[] sprites;
    int animationTick;
    float lastAnimationUpdate;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        float curTime = Time.time;
        while (curTime > lastAnimationUpdate + animationRate)
        {
            lastAnimationUpdate += animationRate;
            animationTick++;
            animationTick %= sprites.Length;
        }

        sr.sprite = sprites[animationTick];

    }
}

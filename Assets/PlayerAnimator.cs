using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public float animationRate = 0.2f;
    public Sprite[] fwdSprites;
    public Sprite[] bckSprites;
    public Sprite[] rightSprites; // Right sprites are just flipped woo
    //public Rigidbody2D rb;
    public PlayerController pc;
    int animationTick;
    float timeSinceLastUpdate;

    // Start is called before the first frame update
    void Start() {
        
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        while (timeSinceLastUpdate > animationRate)
        {
            timeSinceLastUpdate -= animationRate;
            animationTick++;
            animationTick %= fwdSprites.Length;
        }
        // Select texture
        if (pc.acceleration.y < 0)
        {
            sr.sprite = fwdSprites[animationTick];
            sr.flipX = false;
        }
        else if (pc.direction.x > 0)
        {
            sr.sprite = rightSprites[animationTick];
            sr.flipX = false;
        }
        else if (pc.direction.x < 0)
        {
            sr.sprite = rightSprites[animationTick];
            sr.flipX = true;
        }
        else if (pc.direction.y > 0)
        {
            sr.sprite = bckSprites[animationTick];
            sr.flipX = false;
        }
        else { 
            sr.sprite = fwdSprites[0];
            sr.flipX = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public float animationRate = 0.2f;
    public Sprite[] fwdSprites;
    public Sprite[] bckSprites;
    public Sprite[] rightSprites; // Right sprites are just flipped woo
    //public Rigidbody2D rb;
    public EnemyBehavior eb;
    int animationTick;
    float timeSinceLastUpdate;

    // Start is called before the first frame update
    void Start()
    {

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
        Debug.Log(eb.getDeltaPos().x + "  " + eb.getDeltaPos().y);
        if (-eb.getDeltaPos().y > Mathf.Abs(eb.getDeltaPos().x))
        {
            sr.sprite = fwdSprites[animationTick];
            sr.flipX = false;
        }
        else if (eb.getDeltaPos().x > Mathf.Abs(eb.getDeltaPos().y))
        {
            sr.sprite = rightSprites[animationTick];
            sr.flipX = false;
        }   
        else if (-eb.getDeltaPos().x > Mathf.Abs(eb.getDeltaPos().y))
        {
            sr.sprite = rightSprites[animationTick];
            sr.flipX = true;
        }
        else if (eb.getDeltaPos().y > Mathf.Abs(eb.getDeltaPos().x))
        {
            sr.sprite = bckSprites[animationTick];
            sr.flipX = false;
        }
        else
        {
            sr.sprite = fwdSprites[0];
            sr.flipX = false;
        }
    }
}

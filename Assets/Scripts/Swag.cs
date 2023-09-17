using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swag : MonoBehaviour
{

    public int score;
    public int combatType;
    public float startTime;
    public float despawnTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (despawnTime + startTime < Time.time && this != null)
        {
            GameObject.Destroy(this);
            // TODO: Something needs to respawn when it disappears
        }
    }





}

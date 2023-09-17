using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public int scores;
    public float time;
    public float startTime;
    public int timeLimit = 180;
    public Swag_spawner swag_spawner;
    public LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        swag_spawner = GetComponent<Swag_spawner>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = timeLimit - Time.time + startTime;
        if (time < 0) {
            // TODO: Go to ending scene.
        }
    }
}

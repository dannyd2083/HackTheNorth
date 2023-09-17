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
    public bool movementDisabled = false;

    public static Game_Controller instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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

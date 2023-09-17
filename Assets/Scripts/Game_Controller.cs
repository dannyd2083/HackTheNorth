using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = timeLimit - Time.time + startTime;
        if (time <= 0) {
            PlayerPrefs.SetInt("score", scores);
            SceneManager.LoadScene("Ending");
        }
    }
}

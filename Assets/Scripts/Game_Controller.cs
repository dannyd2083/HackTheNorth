using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public int scores;
    public int time;
    public Swag_spawner swag_spawner;

    // Start is called before the first frame update
    void Start()
    {
        swag_spawner = GetComponent<Swag_spawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

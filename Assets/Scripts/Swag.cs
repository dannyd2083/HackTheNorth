using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swag : MonoBehaviour
{

    public int score;
    public int combatType;
    public float despawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time_to_disappear--;
    }

    void dispear()
    {
        if(time_to_disappear <=0 && this != null)
        {
            GameObject.Destroy(this);
        }
    }




}

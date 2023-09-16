using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swag_spawner : MonoBehaviour
{

    public List<Swag> swagList;
    public Swag swag_prefab;
    public int max_number_list;
    public int instantiate_range_x;
    public int instantiate_range_y;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < max_number_list; i++)
        {
            spwan();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spwan()
    {
        Vector3 randmPosition;
        randmPosition.x = Random.Range(1, instantiate_range_x);
        randmPosition.y = Random.Range(1, instantiate_range_y);
        randmPosition.z = 0;
        Instantiate(swag_prefab, randmPosition, Quaternion.identity);
    }
}

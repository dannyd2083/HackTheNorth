using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Swag_spawner : MonoBehaviour
{
   
    public List<Swag> swagList;
    public Swag swag_prefab;
    public int max_number_list;
    public int instantiate_range_x;
    public int instantiate_range_y;
    public int[] value_set;
    public int[] time_set;
    [SerializeField]
    public List<Sprite> sprites;
    public Dictionary <string, Sprite> sprite_dict;

    // Start is called before the first frame update
    void Start()
    {
        if (sprites.Count > 0) {
            if(swagList.Count != sprites.Count)
            {
                Debug.LogError("the sprite number and the swag number is not match");
            }
            for (int i = 0; i< swagList.Count; i++)
            {
                sprite_dict.Add(swagList[i].name, sprites[i]);
            }
        }
        
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
        swag_prefab.swag_value = value_set[Random.Range(0,value_set.Length)];
        swag_prefab.time_to_disappear = time_set[Random.Range(0, time_set.Length)];
    
        if (sprite_dict.Count > 0)
        {
            string sprite_key = sprite_dict.ElementAt(Random.Range(0, sprite_dict.Count)).Key;
            swag_prefab.GetComponent<SpriteRenderer>().sprite = sprite_dict[sprite_key];
        }

        Swag dummy = Instantiate(swag_prefab, randmPosition, Quaternion.identity);
        swagList.Add(dummy);
    }

    public void remove(Swag s)
    {
        swagList.Remove(s);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Swag_spawner : MonoBehaviour
{
    public LevelManager lm;
    public List<Swag> swagList;
    List<SwagData> swagDataList;
    int totalWeight;
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
        // Make sure lm is initialized
        lm.Initialize(1);

        loadInfoFromFile("Assets/Maps/data1.txt");

        // Spawn in a bunch of swag
        for (int i=0; i<max_number_list; i++)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public struct SwagData
    {
        public int value;
        public int combatType;
        public int weight;
        public string name;
    }

    public void loadInfoFromFile(string filePath) {
        StreamReader reader = new StreamReader(filePath);
        string line;
        swagDataList = new List<SwagData>();
        while ((line = reader.ReadLine()) != null)
        {
            //Debug.Log(line);
            string[] data = line.Split(' ');
            int value = int.Parse(data[0]);
            int combatType = int.Parse(data[1]);
            int weight = int.Parse(data[2]);
            string name = data[3].Replace('_', ' ');
            // add the data to swagdatalist
            SwagData swagData = new SwagData();
            swagData.value = value;
            swagData.combatType = combatType;
            swagData.weight = weight;
            swagData.name = name;
            swagDataList.Add(swagData);

            totalWeight += weight;
        }
        reader.Close();
    }

    public void Spawn() {
        Vector2 position = Vector2.zero;
        do {
            position = lm.getRandomPosition();
            foreach (Swag curSwag in swagList) {
                if (((Vector2)curSwag.transform.position - position).magnitude < 1) {
                    continue;
                }
            }
        } while (false);
        Swag s = Instantiate(swag_prefab, position, Quaternion.identity);
        Debug.Log("Spawned!");
        SwagData data = getRandomSwagData();
        s.score = data.value;
        s.combatType = data.combatType;
        s.name = data.name;
        swagList.Add(s);
    }

    SwagData getRandomSwagData() {
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (SwagData swagData in swagDataList)
        {
            currentWeight += swagData.weight;
            if (currentWeight > randomWeight)
            {
                return swagData;
            }
        }
        return swagDataList[0];
    }

    public void Remove(Swag s) {
        swagList.Remove(s);
        //Destroy(s.gameObject);
    }
}

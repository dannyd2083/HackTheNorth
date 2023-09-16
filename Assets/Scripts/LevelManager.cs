using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject wallObj;
    public GameObject tableObj;
    public GameObject chairObj;
    public GameObject floorObj;
    public GameObject spawnerObj;
    public GameObject voidObj;

    List<GameObject> objects = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        Initialize(1);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void Initialize(int level) {
        // Load maps
        loadMapsFromFile("Assets/Maps/Map" + level + ".txt");
        // Load configs - IF TIME PERMITS
    }

    void Clear() {
        foreach (GameObject obj in objects){
            Destroy(obj);
        }
        objects.Clear();
    }

    void loadMapsFromFile(string filePath) {
        StreamReader reader = new StreamReader(filePath);
        string line;
        int y = 0;
        while ((line = reader.ReadLine()) != null) {
            for (int i=0, x=0; i<line.Length; i++, x++) {
                Vector2 position = new Vector2(i, y);
                switch (line[i]) {
                    case 'S':
                        // Create a spawner
                        GameObject spawner = Instantiate(spawnerObj, position, Quaternion.identity);
                        objects.Add(spawner);
                        break;
                    case '#':
                        // Create a wall
                        GameObject wall = Instantiate(wallObj, position, Quaternion.identity);
                        objects.Add(wall);
                        break;
                    case 'T':
                        // Create a table
                        GameObject table = Instantiate(tableObj, position, Quaternion.identity);
                        objects.Add(table);
                        break;
                    case 'C':
                        GameObject chair = Instantiate(chairObj, position, Quaternion.identity);
                        objects.Add(chair);
                        // Create a chair
                        break;
                    case 'V':
                        GameObject void_ = Instantiate(voidObj, position, Quaternion.identity);
                        objects.Add(void_);
                        // Create a void
                        break;
                    case 'P':
                        // Teleport the player here
                        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                        player.transform.position = position;
                        goto case ' ';
                        // Also add a floor tile, so no break here
                    case ' ':
                        GameObject floor = Instantiate(floorObj, position, Quaternion.identity);
                        objects.Add(floor);
                        // Create a floor
                        break;
                    default:
                    break;
                }
            }
            y++;
        }
        reader.Close();
    }
}

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
    public GameObject enemyManager;
    public GameObject swagManager;

    List<GameObject> objects = new List<GameObject>();
    List<Vector2> spawnable = new List<Vector2>();
    bool initialized = false;

    // Start is called before the first frame update
    void Start() {
        if (!initialized) {
            Initialize(1);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Initialize(int level) {
        if (initialized) return;
        initialized = true;
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
        spawnable = new List<Vector2>();
        string line;
        int y = 0;
        while ((line = reader.ReadLine()) != null) {
            for (int x=0; x<line.Length; x++) {
                Vector2 position = new Vector2(x, y);
                switch (line[x]) {
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
                        spawnable.Add(position);
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
                        // Create a floor
                        GameObject floor = Instantiate(floorObj, position, Quaternion.identity);
                        objects.Add(floor);
                        spawnable.Add(position);
                        break;
                    default:
                    break;
                }
            }
            y++;
        }
        reader.Close();
    }

    public Vector2 getRandomPosition()
    {
        return spawnable[Random.Range(0, spawnable.Count)];
    }
}

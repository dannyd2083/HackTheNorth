using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject wallObj;
    public GameObject wallObj3;
    public GameObject wallObj4;
    public GameObject wallObj5;
    public GameObject tableObj;
    public GameObject tableH1Obj;
    public GameObject tableH2Obj;
    public GameObject tableH3Obj;
    public GameObject tableV1Obj;
    public GameObject tableV2Obj;
    public GameObject chairObj;
    public GameObject chairSideObj;
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
                        // Create a wall (upright)
                        GameObject wall = Instantiate(wallObj, position + new Vector2(0.0f, 0.4f), Quaternion.identity);
                        objects.Add(wall);
                        break;
                    case 'L':
                        // Create a wall
                        GameObject wall4L = Instantiate(wallObj4, position, Quaternion.identity);
                        objects.Add(wall4L);
                        break;
                    case 'R':
                        // Create a wall
                        GameObject wall4R = Instantiate(wallObj4, position, Quaternion.Euler(Vector3.forward * 180));
                        objects.Add(wall4R);
                        break;
                    case 'D':
                        // Create a wall
                        GameObject wall4D = Instantiate(wallObj4, position, Quaternion.Euler(Vector3.forward * 270));
                        objects.Add(wall4D);
                        break;
                    case '$':
                        // Create a wall
                        GameObject wall5270 = Instantiate(wallObj5, position, Quaternion.Euler(Vector3.forward * 270));
                        objects.Add(wall5270);
                        break;
                    case '%':
                        // Create a wall
                        GameObject wall5 = Instantiate(wallObj5, position, Quaternion.identity);
                        objects.Add(wall5);
                        break;
                    case 'X':
                        // Wall 3 - Upright
                        GameObject wall3a = Instantiate(wallObj3, position, Quaternion.identity);
                        objects.Add(wall3a);
                        break;
                    case 'Y':
                        // Wall 3 - Sideways
                        GameObject wall3b = Instantiate(wallObj3, position, Quaternion.Euler(Vector3.forward * 90));
                        objects.Add(wall3b);
                        break;
                    case 'T':
                        {
                            // Create a table
                            GameObject table = Instantiate(tableObj, position, Quaternion.identity);
                            objects.Add(table);
                            spawnable.Add(position);
                            break;
                        }
                    case '[':
                        {
                            // Create a table
                            GameObject table = Instantiate(tableH1Obj, position, Quaternion.identity);
                            objects.Add(table);
                            spawnable.Add(position);
                            break;
                        }
                    case '=':
                        {
                            // Create a table
                            GameObject table = Instantiate(tableH2Obj, position, Quaternion.identity);
                            objects.Add(table);
                            spawnable.Add(position);
                            break;
                        }
                    case ']':
                        {
                            // Create a table
                            GameObject table = Instantiate(tableH3Obj, position, Quaternion.identity);
                            objects.Add(table);
                            spawnable.Add(position);
                            break;
                        }
                    case 'C':
                        {
                            // Create a chair
                            GameObject chair = Instantiate(chairObj, position, Quaternion.identity);
                            objects.Add(chair);
                            // Fill in the void
                            goto case 'x';
                        }
                    case '(':
                        {
                            // Create a chair
                            GameObject chair = Instantiate(chairSideObj, position, Quaternion.identity);
                            objects.Add(chair);
                            // Fill in the void
                            goto case 'x';
                        }
                    case ')':
                        {
                            // Create a chair
                            GameObject chair = Instantiate(chairSideObj, position, Quaternion.identity);
                            chair.GetComponent<SpriteRenderer>().flipX = true;
                            objects.Add(chair);
                            // Fill in the void
                            goto case 'x';
                        }
                    case 'V':
                        // Create a void
                        GameObject void_ = Instantiate(voidObj, position, Quaternion.identity);
                        objects.Add(void_);
                        break;
                    case 'P':
                        // Teleport the player here
                        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                        player.transform.position = position;
                        goto case 'x';
                        // Also add a floor tile, so no break here
                    case ' ':
                        // Create a floor
                        GameObject floor = Instantiate(floorObj, position, Quaternion.identity);
                        objects.Add(floor);
                        spawnable.Add(position);
                        break;
                    case 'x':
                        // Create a floor that is not spawnable
                        GameObject floor_ = Instantiate(floorObj, position, Quaternion.identity);
                        objects.Add(floor_);
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

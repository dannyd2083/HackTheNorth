using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnTime;
    public int maxEnemies;
    float lastSpawn = 0f;

    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> spawners = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        spawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("spawner"));
    }

    // Update is called once per frame
    void Update() {
        // Check for dead (destroyed) enemies
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null) {
                enemies.RemoveAt(i);
                i--;
            }
        }

        if (Time.time - lastSpawn > spawnTime && enemies.Count < maxEnemies) {
            lastSpawn = Time.time;
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        // Pick a random spawner
        int spawnerIndex = Random.Range(0, spawners.Count);
        GameObject enemy = Instantiate(enemyPrefab, spawners[spawnerIndex].transform.position, Quaternion.identity);
        enemies.Add(enemy);
    }
}

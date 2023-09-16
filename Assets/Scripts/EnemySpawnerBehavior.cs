using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnTime;
    float lastSpawn = 0f;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - lastSpawn > spawnTime) {
            lastSpawn = Time.time;
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

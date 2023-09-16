using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float defaultAcceleration;
    public float maxX;
    public float maxY;
    public float exitTime; // After 100 seconds, the enemy will leave the HTN Sponsor Area
    public float arrivalLimit; // If the enemy is within this distance of the target, it has arrived and will find a new target
    public float exitLimit; // If the enemy is within this distance of the spawn point, it will exit
    bool exit = false;
    Vector2 targetPos;
    Vector2 originalPos;
    Vector2 spawnPos;
    Rigidbody2D rb;
    double spawnTime;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        acquireTarget();
        spawnTime = Time.time;
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {

        // Target acquisition
        Vector2 currentPos = transform.position;
        Vector2 currentDelta = targetPos - currentPos;
        if (exit && currentDelta.magnitude < exitLimit) {
            // If the player is exiting, destroy the enemy when it reaches the spawn point
            GameObject.Destroy(gameObject);
            return;
        }
        if (currentDelta.magnitude < arrivalLimit) {
            // Otherwise, reacquire the target
            acquireTarget();
        }

        // Actually, try to exit if the target is too far away
        if (Time.time - spawnTime > exitTime) {
            // Set the target to be the spawnpoint (you exit the same way you get in, for now)
            exit = true;
            targetPos = spawnPos;
        }

        // Movement - simple straight line for now
        Vector2 deltaPos = targetPos - (Vector2)transform.position;
        Vector2 acceleration = deltaPos.normalized * defaultAcceleration * Time.deltaTime;
        rb.AddForce(acceleration);
    }

    public void acquireTarget() {
        // For now, use a random target
        targetPos.x = Random.Range(-maxX, maxX);
        targetPos.y = Random.Range(-maxY, maxY);
        // When tables are set up, they will try to find a random table to move to.
        // Also reset original position
        originalPos = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float defaultAcceleration;
    public float maxX;
    public float maxY;
    public float exitTime; // After 100 seconds, the enemy will leave the HTN Sponsor Area
    public float arrivalLimit; // If the enemy is within this distance of the wandering target, it has arrived and will find a new target
                               // Note that other targets use the colliders. This is just for wandering because no object actually exists there.
    GameObject targetObject;
    List<ContactPoint2D> collisionPoints = new List<ContactPoint2D>();
    Vector2 targetPosition;
    Rigidbody2D rb;
    double spawnTime;
    MovementScheme movementScheme;

    enum MovementScheme {
        WANDERING,       // Randomly move around the room, no swag in sight
        SWARMING,        // Swarm towards swag
        RETREATING,      // Move towards the exit
        BROWSING         // Move along tables, looking for swag. NOT IMPLEMENTED - MAYBE LATER
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        acquireTarget();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        // Target acquisition

        if (Time.time - spawnTime > exitTime) {
            // Time to leave
            acquireExit();
        }

        // Check if we reached wander target
        if ((targetPosition - (Vector2)transform.position).magnitude < arrivalLimit && movementScheme == MovementScheme.WANDERING) {
            acquireTarget();
        }
        if (targetObject == null && movementScheme == MovementScheme.SWARMING) {
            acquireTarget();
        }

        // Direction to move in
        Vector2 deltaPos = Vector2.zero;
        switch (movementScheme) {
        case MovementScheme.WANDERING: {
                deltaPos = targetPosition - (Vector2)transform.position;
            }
            break;
        case MovementScheme.SWARMING: {
                deltaPos = (Vector2)targetObject.gameObject.transform.position - (Vector2)transform.position;
            }
            break;
        case MovementScheme.RETREATING: {
                // Simple straight line retreating
                deltaPos = (Vector2)targetObject.gameObject.transform.position - (Vector2)transform.position;
            }
            break;
        case MovementScheme.BROWSING: {
                // NOT IMPLEMENTED - IF WE HAVE TIME!
            }
            break;
        }
        Vector2 acceleration = deltaPos.normalized * defaultAcceleration * Time.deltaTime;

        rb.AddForce(acceleration);
    }

    public void acquireTarget() {
        if (acquireSwag()) return;
        acquireWanderTarget();
    }

    public bool acquireSwag() {
        // Find a random swag
        GameObject[] swag = GameObject.FindGameObjectsWithTag("swag");
        if (swag.Length > 1)
        {
            GameObject newTarget;
            do {
                newTarget = swag[Random.Range(0, swag.Length)];
            } while (newTarget == targetObject);
            targetObject = newTarget;
            targetPosition = targetObject.transform.position;
            movementScheme = MovementScheme.SWARMING;
            return true;
        }
        else {
            // If there is no swag, wander around
            return false;
        }
    }

    public void acquireWanderTarget() {
        // Go to a random, valid tile
        targetPosition = new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
        movementScheme = MovementScheme.WANDERING;
    }

    public void acquireExit() {
        // Find the closest exit
        GameObject[] exits = GameObject.FindGameObjectsWithTag("spawner");
        GameObject closestExit = exits[0];
        float closestDistance = (closestExit.transform.position - transform.position).magnitude;
        foreach (GameObject exit in exits) {
            float distance = (exit.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestExit = exit;
                closestDistance = distance;
            }
        }
        targetObject = closestExit;
        targetPosition = targetObject.transform.position;
        movementScheme = MovementScheme.RETREATING; 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject == targetObject && movementScheme == MovementScheme.SWARMING) {
            // Find someplace new to wander to
            acquireTarget();
        }
        else if (collision.gameObject.tag == "spawner" && movementScheme == MovementScheme.RETREATING) {
            // Bye bye
            Destroy(gameObject);
        }
        else {
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            collision.GetContacts(contacts);
            foreach (ContactPoint2D contactpoint in contacts) {
                collisionPoints.Add(contactpoint);
                break;
            }
        }
    }
}

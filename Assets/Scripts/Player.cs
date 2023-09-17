using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    public float acceleration;
    //Camera viewCamera;
    PlayerController controller;
    Vector2 movement;
    Game_Controller gm;

    public Rigidbody2D body;
    public RhythmController rhythmController;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        //viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        gm = FindObjectOfType<Game_Controller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game_Controller.instance.movementDisabled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            controller.Move(movement * acceleration);
        }
    }

    private IEnumerator RhythmSequence(Swag swagObj)
    {
        yield return rhythmController.RhythmSequence(swagObj.combatType, gm, swagObj.score);
        gm.swag_spawner.Spawn();

        gm.swag_spawner.Remove(swagObj);
        GameObject.Destroy(swagObj.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "swag")
        {
            Swag swagObj = collider.gameObject.GetComponent<Swag>();

            // Rhythm game!
            if (swagObj.combatType != 0)
            {
                //Debug.Log("Enter combat stage " + swagObj.combatType);
                StartCoroutine(RhythmSequence(swagObj));
                return;
            }
            // Otherwise just pick it up & spawn a new one
            
            gm.swag_spawner.Spawn();

            gm.scores += swagObj.score;
            gm.swag_spawner.Remove(swagObj);
            GameObject.Destroy(collider.gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    public float acceleration;
    Camera viewCamera;
    PlayerController controller;
    Vector2 movement;
    Game_Controller gm;

    public Rigidbody2D body;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        gm = FindObjectOfType<Game_Controller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        controller.Move(movement * acceleration);


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "swag")
        {
            Debug.Log("destroy");
            GameObject.Destroy(collider.gameObject);
            gm.scores =+ collider.gameObject.GetComponent<Swag>().swag_value;
            gm.swag_spawner.swagList.Remove(collider.gameObject.GetComponent<Swag>());
            gm.swag_spawner.spawn();
        }

    }
}

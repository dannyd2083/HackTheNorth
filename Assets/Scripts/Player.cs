using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    public float moveSpeed;
    Camera viewCamera;
    PlayerController controller;
    Vector2 movement;
    Game_Controller gm;

    // Start is called before the first frame update
    protected override void Start()
    {

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
        controller.Move(movement * moveSpeed);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "swag")
        {
            GameObject.Destroy(collision.gameObject);
            gm.scores =+ collision.gameObject.GetComponent<Swag>().swag_value;
            gm.swag_spawner.swagList.Remove(collision.gameObject.GetComponent<Swag>());
        }

    }
}

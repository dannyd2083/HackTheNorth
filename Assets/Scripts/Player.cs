using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

    public float acceleration;
    Camera viewCamera;
    PlayerController controller;
    Vector2 movement;

    public Rigidbody2D body;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        controller.Move(movement * acceleration);


    }
}

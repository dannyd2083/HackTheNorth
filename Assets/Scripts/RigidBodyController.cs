using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    public float movementSpeed;

    private Rigidbody2D rb;
    private Vector2 inputs;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputs = Vector2.zero;
        inputs.x = Input.GetAxisRaw("Horizontal");
        inputs.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //rb.velocity = inputs * movementSpeed;
        rb.MovePosition(rb.position + inputs * movementSpeed * Time.fixedDeltaTime);
    }

}
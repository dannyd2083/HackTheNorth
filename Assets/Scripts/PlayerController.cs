using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 acceleration;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
      

    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.AddForce(acceleration);
        rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
        acceleration = Vector2.zero;
    }

    public void Move(Vector2 _acceleration)
    {
        acceleration = _acceleration;
    }
}

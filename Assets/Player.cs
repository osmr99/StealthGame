using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector2 movement;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>() * speed;
    }
}

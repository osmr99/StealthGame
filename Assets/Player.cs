using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public Vector2 movement;
    public int speed;
    public bool isSneaking = false;

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
        if(isSneaking)
        {
            rb.velocity = new Vector3(movement.x * 0.65f, rb.velocity.y, movement.y * 0.65f);
        }
        else
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.y);
        }
        
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>() * speed;
    }

    public void OnSneak()
    {
        isSneaking = !isSneaking;
    }
}

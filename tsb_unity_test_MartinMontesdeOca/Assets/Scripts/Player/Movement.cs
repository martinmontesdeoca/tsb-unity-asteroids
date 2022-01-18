using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float acceleration = 4;
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (move.magnitude > 0)
        {
            Move(move, Input.GetAxis("Vertical"));
            Rotation(move, Input.GetAxis("Horizontal"));
        }
        
    }

    private void Rotation(Vector2 moveData, float horizontal)
    {      
        if (horizontal == 0) return;        
        transform.Rotate(0, 0, -90 * horizontal * Time.deltaTime);
    }

    private void Move(Vector2 moveData, float vertical)
    {
        var stabilizePosition = Mathf.Clamp(vertical, 0f, 1f);
        rigidBody.AddForce(transform.up * acceleration * stabilizePosition);        
        if (rigidBody.velocity.magnitude > 4)
            rigidBody.velocity = rigidBody.velocity.normalized * 4;

        rigidBody.angularVelocity = 0f;
    }
}

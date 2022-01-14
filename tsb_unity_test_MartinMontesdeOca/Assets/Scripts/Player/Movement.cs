using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool onPlay = true;
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float acceleration = 4;
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (onPlay)
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (move.magnitude > 0)
            {
                Move(move, Input.GetAxis("Vertical"));
                Rotation(move, Input.GetAxis("Horizontal"));
            }
        }
    }

    private void Rotation(Vector2 moveData, float horizontal)
    {
        // float angle = Mathf.Atan2(moveData.x, moveData.y);
        //transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.back);
        if (horizontal == 0) return;        
        transform.Rotate(0, 0, -90 * horizontal * Time.deltaTime);
    }

    private void Move(Vector2 moveData, float vertical)
    {
        var stabilizePosition = Mathf.Clamp(vertical, 0f, 1f);
        rigidBody.AddForce(transform.up * acceleration * stabilizePosition);
        // rigidBody.AddForce(moveData * acceleration);
        if (rigidBody.velocity.magnitude > 4)
            rigidBody.velocity = rigidBody.velocity.normalized * 4;

        rigidBody.angularVelocity = 0f;
    }
}

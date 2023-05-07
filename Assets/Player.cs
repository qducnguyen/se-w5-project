using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GrappleHook gh;
    public float gravity;
    public Vector2 velocity;
    public float groundHeight = 10;
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    private float dirX = 0f;
    public float distance = 0f;
    void Start()
    {
        gh = GetComponent<GrappleHook>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        velocity.x = dirX * moveSpeed;
    }

    private void FixedUpdate() 
    {
        Vector2 pos = transform.position;
        if (!isGrounded) 
        {
            // pos.y -= velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
            pos.x += velocity.x * Time.fixedDeltaTime;

            
            // Vector2 rayOrigin = new Vector2(pos.x + 1f, pos.y);
            // Vector2 rayDirection = Vector2.up;
            // float rayDistance = velocity.y * Time.fixedDeltaTime;
            // RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            // if (hit2D.collider != null)
            // {
            //     Ground ground = hit2D.collider.GetComponent<Ground>();
            //     Debug.Log("found ground");
            //     if (ground != null)
            //     {
            //         groundHeight = ground.groundHeight;
            //         pos.y = groundHeight;
            //         velocity.y = 0f;
            //         isGrounded = true;
            //     }
            // }
            // Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
        }

        transform.position = pos;
        distance += velocity.y * Time.fixedDeltaTime;

        if (!gh.retracting) {
            velocity = new Vector2(dirX * moveSpeed, velocity.y);
        } else {
            velocity = Vector2.zero;
        }

    }
}

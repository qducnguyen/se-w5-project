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
        velocity.y += gravity * Time.fixedDeltaTime;
        pos.x += velocity.x * Time.fixedDeltaTime;


        transform.position = pos;
        distance += velocity.y * Time.fixedDeltaTime;

        if (!gh.retracting) {
            velocity = new Vector2(dirX * moveSpeed, velocity.y);
        } else {
            velocity = Vector2.zero;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public float gravity = 3;

    // BUG
    public Vector2 velocity;
    public float groundHeight = 10;
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public float distance = 0f;

    // private float dirX = 0f;
    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();   
    }
    void Start()
    {
        // Just work for transform, I do not understand yet. 
        // rb2d.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // dirX = Input.GetAxis("Horizontal");
        // velocity.x = dirX * moveSpeed; //BUG
    }

    private void FixedUpdate() 
    {
        // Vector2 pos = transform.position;
        // velocity.y += gravity * Time.fixedDeltaTime;
        // pos.x += velocity.x * Time.fixedDeltaTime; // bug?


        // transform.position = pos;
        // distance += velocity.y * Time.fixedDeltaTime;
        distance += -rb2d.velocity.y * Time.fixedDeltaTime;

    }
}

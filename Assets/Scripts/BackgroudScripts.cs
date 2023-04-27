using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudScripts : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private float height, width;
    private float speed = 0;
    private float initial_speed = -2f;
    private float passed_time = 0f;
    private float acceleration = -1f;
    private Score scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        height = boxCollider.size.y;
        width = boxCollider.size.x;
        rb.velocity = new Vector2(0, speed);

    }

    void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<Score>();
    }

    void UpdateVelocity() {
        passed_time += Time.deltaTime;
        speed = initial_speed + acceleration * passed_time * passed_time / 2;
        rb.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.position.y);
        if (transform.position.y < -height) {
            Reposition();
        }
        
        UpdateVelocity();
        scoreManager.UpdateScore(calculateScore());
    }

    private void Reposition() {
        Vector2 vector = new Vector2(0, height * 2f);
        transform.position = (Vector2) transform.position + vector;
    }

    private int calculateScore()
    {
        return -1 * (int)(acceleration * passed_time * passed_time / 2 + passed_time * initial_speed);
    }
}

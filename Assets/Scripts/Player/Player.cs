using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float distance = 0f;

    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate() 
    {
        distance += -rb2d.velocity.y * Time.fixedDeltaTime;
    }
}

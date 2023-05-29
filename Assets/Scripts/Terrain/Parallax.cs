using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;
    Player player;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.y / depth;
        Vector2 pos = transform.position;

        pos.y += realVelocity * Time.fixedDeltaTime;

        if (pos.y >= Camera.main.transform.position.y + 17f) 
        {
            pos.y = Camera.main.transform.position.y - 17f;
        }

        transform.position = pos;
    }
}

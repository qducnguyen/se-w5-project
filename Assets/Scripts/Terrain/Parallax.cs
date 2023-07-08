using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // [SerializeField] private int depth = 1;
    [SerializeField] private float maxHeight = 18;
    [SerializeField] private float objectHeight = 0;
    [SerializeField] private Player player;

    void FixedUpdate()
    {
        // float realVelocity = player.velocity.y / depth;
        Vector2 pos = transform.position;

        // pos.y += realVelocity * Time.fixedDeltaTime;

        if (pos.y >= Camera.main.transform.position.y + maxHeight + objectHeight / 2 ) 
        {
            pos.y = Camera.main.transform.position.y - maxHeight - objectHeight / 2;
        }

        else if (pos.y < Camera.main.transform.position.y - maxHeight - objectHeight / 2)
        {
            pos.y = Camera.main.transform.position.y + maxHeight + objectHeight / 2;
        }


        transform.position = pos;
    }
}

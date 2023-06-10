using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float yDistance;

    private void Start() {
        yDistance =  transform.position.y- player.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y + yDistance, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform background;

    void Update()
    {
        transform.position = new Vector3(background.position.x, player.position.y, transform.position.z);
    }
}

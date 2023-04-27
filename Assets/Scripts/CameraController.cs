using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private Transform background;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(background.position.x, player.position.y, transform.position.z);
    }
}

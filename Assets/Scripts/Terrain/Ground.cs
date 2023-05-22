using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundBottom;
    public float screenBottom;
    BoxCollider2D collider;

    bool didGenerateGround = false;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        screenBottom = Camera.main.transform.position.y - 16f;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.y += player.velocity.y * Time.fixedDeltaTime;

        groundBottom = transform.position.y - (collider.size.y / 2);
        screenBottom = Camera.main.transform.position.y - 16f;

        if ((groundBottom > Camera.main.transform.position.y + 30f)) 
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround) 
        {
            if (groundBottom > screenBottom)
            {
                didGenerateGround = true;
                generateGround();
            }
        }
        transform.position = pos;
    }

    void generateGround() {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;
        pos.x = Random.Range(-6f, 6f);
        pos.y = transform.position.y - Random.Range(5f, 10f);
        go.transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    LineRenderer line;

    Player player;
    Transform transform;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 30f;
    [SerializeField] float grappleSpeed = 20f;
    [SerializeField] float grappleShootSpeed = 20f;

    public bool isGrappling = false;
    private float screenBottom = 0f;
    private float screenTop = 0f;
    [HideInInspector] public bool retracting = false;
    Vector2 target;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        transform = GameObject.Find("Player").GetComponent<Transform>();
        screenBottom = Camera.main.transform.position.y - 16f;
        screenTop = Camera.main.transform.position.y + 16f;
    }


    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update() 
    {
        screenBottom = Camera.main.transform.position.y - 16f;
        screenTop = Camera.main.transform.position.y + 16f;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);
        target = hit.point + player.velocity * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartGrapple();
        }

        if (retracting) 
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            transform.position = grapplePos;
            line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }

        }
    }
    

    private void StartGrapple() 
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        Debug.Log(direction);

        if ((hit.collider != null) && (target.y > screenBottom) && (target.y < screenTop))
        {
            isGrappling = true;
            // target = hit.point;
            line.enabled = true;
            line.positionCount = 2;
            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple() 
    {
        float t = 0;
        float time = 10;
        
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime) 
        {
            newPos = Vector2.Lerp(transform.position, target, t/time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] LayerMask grapmask;
    [SerializeField] float maxdistance = 10f;
    [SerializeField] float grapplespeed = 10f;
    [SerializeField] float grapleshootspeed = 20f;

    bool isgrappling = false;
    [HideInInspector] public bool retracting = false;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }

    Vector2 target;
    private float holdingTime;
    private float cooldownTime = 0;
    
    private void Start () {
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        Debug.Log(rb.gravityScale);
        cooldownTime -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !isgrappling && cooldownTime < 0) {
            StartGrapple();
            rb.gravityScale = 0;
            holdingTime = 0;
            cooldownTime = 2;
        }

        if (retracting) {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grapplespeed * Time.deltaTime);
            transform.position = grapplePos;
            line.SetPosition(0, transform.position);
            holdingTime += Time.deltaTime;
            if (Vector2.Distance(transform.position, target) < .5f || holdingTime > .3) {
                retracting = false;
                isgrappling = false;
                line.enabled = false;
                rb.gravityScale = 1;
            }
            
        }

        else {
            dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
        }
    }

    private void StartGrapple() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxdistance, grapmask);

        if (hit.collider != null) {
            isgrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grappe());
        }
    }

    IEnumerator Grappe() {
        float t = 0;
        float time = 10;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPosition;
        for (; t<time; t += grapleshootspeed * Time.deltaTime) {
            newPosition = Vector2.Lerp(transform.position, target, t/ time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPosition);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }

    private void UpdateAnimationState() 
    {
        MovementState state;

        if (dirX > 0f) 
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) 
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f) {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f) 
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    
}

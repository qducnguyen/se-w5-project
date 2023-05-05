using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    // Parameters
    [SerializeField] float maxdistance = 10f;
    [SerializeField] float grapplespeed = 10f;
    [SerializeField] float grapleshootspeed = 20f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 7f;
    private float maxholdingtime = 1000f;
    private float mindistance = .5f;
    private Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    //
    LineRenderer line;
    private Rigidbody2D rb;
    private HingeJoint2D hj;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    bool isgrappling = false;
    [HideInInspector] public bool retracting = false;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    private enum MovementState { idle, running, jumping, falling }
    Vector2 target;
    private float holdingTime;
    private float cooldownTime = 0;
    private Vector3 direction = Vector3.forward;

    // public float moveSpeed = 10f;
    public float leftAngle = -0.35f;
    public float rightAngle = 0.35f;

    bool movingClockwise;
    
    private void Start () {
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        movingClockwise = true;
        // fixedpoint = new Vector3 (rb.position.x + 1, rb.position.y + 1, 0);
    }

    private void Update() {
        cooldownTime -= Time.deltaTime;
        // transform.RotateAround(fixedpoint , new Vector3 (0,0,1f), grapplespeed * 10 * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && !isgrappling && cooldownTime < 0) {
            StartGrapple();
            rb.gravityScale = 0;
            holdingTime = 0;
            cooldownTime = 2;
        }

        if (retracting) {
            // Vector2 grapplePos = Vector2.Lerp(transform.position, target, grapplespeed * Time.deltaTime);
            // transform.position = grapplePos;

            

            // if (IsLeft()) {
            //     direction = Vector3.back;
            // }

            // if (IsRight()) {
            //     direction = Vector3.forward;
            // }
            Debug.Log(transform.rotation);
            transform.RotateAround(target , direction, 1);
            line.SetPosition(0, transform.position);

            holdingTime += Time.deltaTime;
            if (Vector2.Distance(transform.position, target) < mindistance || holdingTime > maxholdingtime) {
                retracting = false;
                isgrappling = false;
                line.enabled = false;
                
            }
            
        }

        // else {
        //     rb.gravityScale = 1;
        //     dirX = Input.GetAxis("Horizontal");

        //     if (Input.GetButtonDown("Jump") && IsGrounded())
        //     {
        //         rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
            
        //     }

        //     if ((IsLeft() && dirX < 0) || (IsRight() && dirX > 0)) {
        //         rb.gravityScale = 0.7f;
        //         rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        //     }

        //     else {
        //         rb.gravityScale = 1f;
        //         rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        //     }

        //     UpdateAnimationState();

            
        // }
    }

    private void StartGrapple() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxdistance, jumpableGround);

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

    public bool IsLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.1f, jumpableGround)
                ;
    }

    public bool IsRight()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.1f, jumpableGround)
                ;
    }

    
    public void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }

    }

    public void Move()
    {
        ChangeMoveDir();

        if (movingClockwise)
        {
            rb.angularVelocity = moveSpeed;
        }

        if (!movingClockwise)
        {
            rb.angularVelocity = -1*moveSpeed;
        }
    }
}

using UnityEngine;
using System.Collections;

public class GrapplingGun : MonoBehaviour
{   

    [HideInInspector] public static GrapplingGun Instance; 

    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform hook;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;

    public Player player;

    public Rigidbody2D RbPlayer;


    [SerializeField] private float init_gravity;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [Header("Sound:")]
    [SerializeField] private AudioSource grappleSound;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1f;
    [SerializeField] private float launchSpeedMonster = 0.5f;

    private float launchSpeedCurrent;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [Header("Is Grappling in the Middle of Object")]
    [SerializeField] private bool isMiddleObject = false;

    [Header("Timeout for Shooting")]
    [SerializeField] private float timeout = 0.4f;
    private float timeoutCount;

    [SerializeField] private float grapplingTime = 3.0f;
    private float grapplingTimeCount;

    [SerializeField] private float grapplingTimeMonster = 0.1f;
    private float grapplingTimeMonsterCount;

    [HideInInspector] public bool isGrapplingMonster;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    private  GameObject objectHitted;

    private Touch touch;

    private void Awake() {
        Instance = this;
    }

    private void Start()
    {

        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        isGrapplingMonster = false;

    }
    private void Update()
    {   

        // Timeout monster
        if (isGrapplingMonster){
            grapplingTimeMonsterCount += Time.deltaTime;
            if (grapplingTimeCount >= grapplingTimeMonster){
                grappleRope.enabled = false;
                m_springJoint2D.enabled = false;
                isGrapplingMonster = false;
            }
        }
        else{
            grapplingTimeMonsterCount = 0;
        }

        // Timeout base checkout 
        if (grappleRope.enabled){
            timeoutCount = 0;
            grapplingTimeCount += Time.deltaTime;
        }
        else{
            hook.position = gunHolder.position;
            timeoutCount += Time.deltaTime;
            grapplingTimeCount = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) 
            && !grappleRope.enabled
            && timeoutCount >= timeout)
        {
            // Debug.Log("Hello in Get Key Down");
            SetGrapplePoint();

        }

        else if ((Input.GetKeyDown(KeyCode.Mouse0) || grapplingTime <= grapplingTimeCount)  
            && grappleRope.enabled )
        {
            // Debug.Log("Hello in Get Key Down and when grapply Rope enabled");

            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;

            if(launchType == LaunchType.Transform_Launch)
            {                
                RbPlayer.gravityScale = 1;
            }
        }

        else if (grappleRope.enabled)
        {
            // Debug.Log("Hello in Get Key");
            if (grappleRope.enabled)
            { 
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true); 
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }  
            }
        }

        
        else
        {
            // Debug.Log("Hello in other keys");
            hook.position = gunHolder.position;
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
        
      
    }


    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (_hit.transform.gameObject.GetComponent<TerrainType>().terrainType == TerrainType.TerrainTypes.monster){
                    launchSpeedCurrent = launchSpeedMonster;
                }
                else{
                    launchSpeedCurrent = launchSpeed;
                }
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    if (!isMiddleObject)
                    {
                        grapplePoint = _hit.point;
                    }
                    else
                    {
                        objectHitted = _hit.collider.gameObject;
                        grapplePoint = objectHitted.GetComponent<BoxCollider2D>().bounds.center;
                    }

                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        grappleSound.Play();
        m_springJoint2D.autoConfigureDistance = false;
        
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }

        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 firePointDistanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = firePointDistanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeedCurrent;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    // player.velocity = Vector2.zero;
                    // player.gravity = 0;
                    RbPlayer.gravityScale = 0;
                    RbPlayer.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}

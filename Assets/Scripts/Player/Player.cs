using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float distance = 0f;

    private Rigidbody2D rb2d;

    [Header("Scaling Difficulty for Gravity")]
    [SerializeField] private float scalingMultiplier;
    [SerializeField] private float timerMax;
    [SerializeField] private float scaledTimerMax;
    [SerializeField] private float gravity;
    [SerializeField] private float scaledGravity;
    [SerializeField] private bool maxGravityReached;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>(); 
        // Difficult Scaling 
        gravity = rb2d.gravityScale;
        scaledGravity = timerMax / scaledTimerMax * gravity; 
    }

    private void FixedUpdate() 
    {
        distance += -rb2d.velocity.y * Time.fixedDeltaTime;
        ScaleGravity();
    }

    private void ScaleGravity() {
        if (maxGravityReached || Time.frameCount % 5 != 0 || Time.timeScale == 0)
            return;

        if (timerMax > scaledTimerMax)
            timerMax /= scalingMultiplier;
        else
            timerMax = scaledTimerMax;

        if (gravity < scaledGravity)
            gravity *= scalingMultiplier;
        else
            gravity = scaledGravity;

        if (timerMax == scaledTimerMax && gravity == scaledGravity)
            maxGravityReached = true;

        rb2d.gravityScale = gravity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private float jumpForce = 60f;
    private float checkRadius = 0.5f;
    private float movementSmoothing = 0.05f;
    private bool isFacingRight = true;
    private bool isOnGround = true;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D playerRb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public UnityEvent OnLandEvent;
    public void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
        if(OnLandEvent == null) {
            OnLandEvent = new UnityEvent();
        }
    }
    public void FixedUpdate() {
        bool wasGrounded = isOnGround;
        isOnGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++) {
            if(colliders[i].gameObject != gameObject) {
                isOnGround = true;
                if (!wasGrounded) {
                    OnLandEvent.Invoke();
                }
            }
        }
    }
    public void Move(float move, bool jump) {
        if (isOnGround) {
            Vector3 targetVelocity = new Vector2(move * 10f, playerRb.velocity.y);
            playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref velocity, movementSmoothing);
            if(move > 0 && !isFacingRight) {
                Mirroring();
            } else if(move < 0 && isFacingRight) {
                Mirroring();
            }
        }
        if(isOnGround && jump) {
            isOnGround = false;
            playerRb.AddForce(new Vector2(0f, jumpForce));
        }
    }
    void Mirroring() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

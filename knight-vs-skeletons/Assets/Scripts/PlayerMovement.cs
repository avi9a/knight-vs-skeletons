using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    public Joystick joystick;
    public Transform attackPoint;
    private Rigidbody2D playerRb;
    public LayerMask enemyLayers;
    public float attackRange = 0.2f;
    private float speed = 45f;
    private float horizontalInput = 0f;
    private bool jump = false;
    public int attackDamage = 10;
    public int maxHealth = 500;
    private int health;
    private GameObject joystickCanvas;
    private void Start() {
        playerRb = GetComponent<Rigidbody2D>();
        joystickCanvas = GameObject.Find("Joystick");
        joystickCanvas.SetActive(true);
        health = maxHealth;
    }
    void Update()
    {
        if (joystick.Horizontal >= 0.2f) {
            horizontalInput = speed;
        } else if(joystick.Horizontal <= -0.2f) {
            horizontalInput = -speed;
        }
        else {
            horizontalInput = 0f;
        } 
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("vSpeed", playerRb.velocity.y);
        float verticalInput = joystick.Vertical;
        if (verticalInput >= 0.5f) {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        Attack();
    }
    public void OnLending() {
        animator.SetBool("IsJumping", false);
    }
    private void FixedUpdate() {
        playerController.Move(horizontalInput * Time.fixedDeltaTime, jump);
        jump = false;
    }
    public void Attack() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2) {
                animator.SetTrigger("Attack1");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach(Collider2D enemy in hitEnemies) {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
        } 
    }
    public void TakeDamageFromEnemies(int playerDamage) {
        health -= playerDamage;
        animator.SetTrigger("Hurt");
        if(health <= 0) {
            Die();
            FindObjectOfType<GameManager>().EndLevel();
            FindObjectOfType<GameManager>().Restart();
            joystickCanvas.SetActive(false);
        }
    }
    void Die() {
        animator.SetBool("IsDead", true);
    }
    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

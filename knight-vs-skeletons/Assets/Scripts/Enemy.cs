using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private float speed = 10;
    public float attackRange = 0.2f;
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;
    private Rigidbody2D enemyRb;
    public Animator animator;
    private GameObject player;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private IEnumerator coroutine;
    public GameManager gameManager;
    void Start() {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(9, 9, true);
    }
    void Update() {
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
        animator.SetFloat("Speed", speed);
    }
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) {
            Die();
        }
    }
    void Die() {
        Debug.Log("Enemy died");
        animator.SetBool("IsDead", true);
        coroutine = DiedAndDisappeared(2);
        StartCoroutine(coroutine);
    }
    IEnumerator DiedAndDisappeared(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            Debug.Log("Enemy tought the player");
            animator.SetTrigger("Attack_e");
            AttackPlayer();
        }
    }
    public void AttackPlayer() {
        if (gameManager.gameOver == false) {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D player in hitPlayer) {
                player.GetComponent<PlayerMovement>().TakeDamageFromEnemies(attackDamage);
            }
        }
    }
    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

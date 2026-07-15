using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canJump = true;
    [SerializeField] private GameObject hitEffectPrefab;

    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;

    [Header("Ranged Attack")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float arrowSpeed = 10f;
    [SerializeField] private float arrowSpeedMultiplier = 0.5f;

    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    private float xInput;
    private bool facingRight = true;
    private bool canMove = true;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) TryToJump();
        if (Input.GetKeyDown(KeyCode.Mouse0)) TryToAttack();
        if (Input.GetKeyDown(KeyCode.Mouse1)) TryToShoot();
    }

    private void TryToAttack()
    {
        if (isGrounded) anim.SetTrigger("attack");
    }

    private void TryToShoot()
    {
        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector2 shootDirection = (mousePos - arrowSpawnPoint.position).normalized;

            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            Rigidbody2D rbArrow = arrow.GetComponent<Rigidbody2D>();
            if (rbArrow != null)
            {
                rbArrow.bodyType = RigidbodyType2D.Dynamic;
                rbArrow.gravityScale = 0;
                rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;
            }

            Destroy(arrow, 5f);
        }
    }

    public void DamageEnemies()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);
        foreach (Collider2D enemy in enemyColliders)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage();
                if (hitEffectPrefab != null) Instantiate(hitEffectPrefab, enemy.transform.position, Quaternion.identity);
            }
        }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    private void TryToJump()
    {
        if (isGrounded && canJump) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(canMove ? xInput * moveSpeed : 0, rb.linearVelocity.y);
    }

    private void HandleCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGrounded);
        isGrounded = hit.collider != null;
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void HandleFlip()
    {
        if ((rb.linearVelocity.x > 0 && !facingRight) || (rb.linearVelocity.x < 0 && facingRight)) Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (arrowSpawnPoint != null) { Gizmos.color = Color.red; Gizmos.DrawSphere(arrowSpawnPoint.position, 0.2f); }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

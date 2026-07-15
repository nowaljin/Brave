using System.Collections.Generic;
rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

public class Player : MonoBehaviour
{
    private Animator anim;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    [Header("Ranged Attack")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float arrowSpeed = 10f;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    private float xInput;
    private bool facingRight = true;
    private bool canMove = true;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGrounded;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) TryToJump();
        if (Input.GetKeyDown(KeyCode.Mouse0)) TryToAttack();
        if (Input.GetKeyDown(KeyCode.Mouse1)) TryToShoot();
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void TryToAttack()
    {
        if (isGrounded) anim.SetTrigger("attack");
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void TryToShoot()
    {
        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector2 shootDirection = (mousePos - arrowSpawnPoint.position).normalized;
            rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

            GameObject arrow = Instantiate(arrowPrefab, spawnPos, Quaternion.identity);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

            Rigidbody2D rbArrow = arrow.GetComponent<Rigidbody2D>();
            if (rbArrow != null)
            {
                rbArrow.bodyType = RigidbodyType2D.Dynamic;
                rbArrow.gravityScale = 0;
                rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

            Destroy(arrow, 5f);
        }
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

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
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void TryToJump()
    {
        if (isGrounded && canJump) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(canMove ? xInput * moveSpeed : 0, rb.linearVelocity.y);
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void HandleFlip()
    {
        if ((rb.linearVelocity.x > 0 && !facingRight) || (rb.linearVelocity.x < 0 && facingRight)) Flip();
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    rbArrow.linearVelocity = shootDirection * arrowSpeed * arrowSpeedMultiplier;

    private void OnDrawGizmosSelected()
    {
        if (arrowSpawnPoint != null) { Gizmos.color = Color.red; Gizmos.DrawSphere(arrowSpawnPoint.position, 0.2f); }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
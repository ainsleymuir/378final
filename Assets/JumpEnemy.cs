using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float range;

    private float startY;
    private bool movingUp = true;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private AudioSource attackSoundEffect;

    private EnemyPatrol enemyPatrol;

    // Reference
    private Animator anim;
    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Start()
    {
        // Store the initial Y position
        startY = transform.position.y;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                DamagePlayer();
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

        // Calculate the target Y position based on the current direction
        float targetY = movingUp ? startY + range : startY - range;

        // Calculate the new position with vertical movement
        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.MoveTowards(newPosition.y, targetY, verticalSpeed * Time.deltaTime);

        // Update the position
        transform.position = newPosition;

        // Check if reached the target position
        if (Mathf.Approximately(newPosition.y, targetY))
        {
            // Change direction
            movingUp = !movingUp;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance,
                                    new Vector3(boxCollider.bounds.size.x * range,
                                                boxCollider.bounds.size.y,
                                                boxCollider.bounds.size.z),
                                            0,
                                            Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance,
                             new Vector3(boxCollider.bounds.size.x * range,
                                                boxCollider.bounds.size.y,
                                                boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        // If player is still in range, damage them
        if (PlayerInSight())
        {
            // Damage Player Health
            attackSoundEffect.Play();
            playerHealth.TakeDamage(damage);
        }
    }
}

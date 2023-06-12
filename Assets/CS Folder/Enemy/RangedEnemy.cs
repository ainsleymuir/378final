using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header ("Ranged Attack")]
    [SerializeField] private Transform frogAttack;
    [SerializeField] private GameObject[] frogAttacks;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private AudioSource attackSoundEffect;

    private EnemyPatrol enemyPatrol;

    //Reference
    private Animator anim;
    private Health playerHealth;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

        private void Update() 
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack() {
        cooldownTimer = 0;
        frogAttacks[findFrogAttack()].transform.position = frogAttack.position;
        frogAttacks[findFrogAttack()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

        private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance, 
                                    new Vector3(boxCollider.bounds.size.x * range, 
                                                boxCollider.bounds.size.y,
                                                boxCollider.bounds.size.z),   
                                            0, 
                                            Vector2.left, 0, playerLayer);
        
        return hit.collider != null;
    }

    private int findFrogAttack() {
        for (int i = 0; i < frogAttacks.Length; i++) {
            if(!frogAttacks[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance, 
                             new Vector3(boxCollider.bounds.size.x * range, 
                                                boxCollider.bounds.size.y,
                                                boxCollider.bounds.size.z));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioSource throwSoundEffect;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private AmmoManager ammoManager;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        ammoManager = GetComponent<AmmoManager>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovement.canAttack() && ammoManager.ammoCount > 0)
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {

        ammoManager.decreaseAmmo();
        
        anim.SetTrigger("attack");

        throwSoundEffect.Play();

        cooldownTimer = 0;
        //pool sparks

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

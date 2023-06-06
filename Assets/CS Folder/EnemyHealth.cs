using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    //How much health player has at the start of the game
    [SerializeField] private float startingHealth;

   //Current health for the player
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;

    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage) 
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            //iframes
        }
        else 
        {
            //player dead
            if (!dead) 
            {
                anim.SetTrigger("die");

                //Player

                PlayerMovement playerMovement_ = GetComponent<PlayerMovement>();

                if(playerMovement_ != null) 
                {
                    playerMovement_.enabled = false;
                }
                
                //Enemy

                EnemyPatrol enemyPatrol_ = GetComponentInParent<EnemyPatrol>();

                if(enemyPatrol_ != null) 
                {
                    enemyPatrol_.enabled = false;
                }

                MeleeEnemy meleeEnemy = GetComponent<MeleeEnemy>();

                if(meleeEnemy != null)
                {
                    meleeEnemy.enabled = false;
                }

                dead = true;

            }
            
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }



}


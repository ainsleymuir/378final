using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if ((collision.gameObject.CompareTag("Apple")) ||  (collision.gameObject.CompareTag("Orange")))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
        }
        
    }
}

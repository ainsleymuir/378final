using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
    //attach this script to each collectable rock in level
    private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.tag == "Player")
        {
            collision.GetComponent<AmmoManager>().addAmmo();
            gameObject.SetActive(false);
        }
   }
}

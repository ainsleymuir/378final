using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void onTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x) {
                cam.MoveToNewRoom(nextRoom);
            }

            else {
                cam.MoveToNewRoom(previousRoom);
            }
        }
    }
  
}


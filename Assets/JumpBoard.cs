using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    private bool canJump = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D objectRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (objectRigidbody != null && canJump)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            objectRigidbody.velocity = jumpVelocity;
            canJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = true;
    }
}

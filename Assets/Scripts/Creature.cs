using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float speed = 2.5f;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector3 offset)
    {
        if (offset != Vector3.zero)
        {
            offset.Normalize();
            Vector3 vel = offset *= speed;

            rb.velocity = vel;

            if (offset.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {

        rb.velocity = Vector3.zero;

    }

    public void MoveToward(Vector3 position)
    {
        Move(position - transform.position);
    }


}

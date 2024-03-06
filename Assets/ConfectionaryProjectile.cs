using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfectionaryProjectile : MonoBehaviour
{
   Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Sharonn" && other.gameObject.tag != "Pineapple")
        {
             Destroy(this.gameObject);
        }
    }

}


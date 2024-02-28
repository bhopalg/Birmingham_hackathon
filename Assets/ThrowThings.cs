using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;


public class ThrowThings : MonoBehaviour
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
        if (other.gameObject.tag == "Sharonn")
        {
            MoveTom.instance.IncreaseScore(1);
            Destroy(this.gameObject);
        } else if (other.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }

}

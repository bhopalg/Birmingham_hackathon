using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private bool isActive = false;
    public GameObject chairPrefab;

    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {

        // If the player is still alive, the chair will fire every 25 frames
        timer++;
        if (timer % 25 == 0) {
            FireChair();
        }
    }

    private void FireChair() {
        Vector2 direction = transform.up;
        float force = 300;
        GameObject projectile = Instantiate(this.gameObject, transform.position + (Vector3)direction * 0.5f, Quaternion.identity);
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //   Destroy(this.gameObject);
    }
}

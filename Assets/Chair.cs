using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public static Chair instance { get; private set; }

    public GameObject chairPrefab;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FireChair() {
        Vector2 direction = transform.up;
        float force = 300;
        GameObject projectile = Instantiate(this.gameObject, transform.position + (Vector3)direction * 0.5f, Quaternion.identity);
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
public class SharonBehaviour : MonoBehaviour
{

    private GameObject playerTom;

    public GameObject floorConePrefab;

    public GameObject marsBarPrefab;

    public GameObject cocktailPrefab;

    private static Random rnd = new Random();

    private float lastAttackTime;
    private    float creationTime = 0.0f;
    private float age = 0.0f;

    private enum AttackType {
        Alcohol,
        FloorCone,
        MarsBar
    }

    void Start()
    {
        Debug.Log("Starting Sharon!");
        creationTime = Time.realtimeSinceStartup;
        lastAttackTime = 0;
        playerTom = GameObject.FindGameObjectWithTag("Player");
    }

    void LayFloorCone()
    {
        Vector2 direction = transform.up;
        GameObject cone = Instantiate(floorConePrefab, transform.position + (Vector3)direction * 0.5f, Quaternion.identity);
        cone.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.age=Time.realtimeSinceStartup-creationTime;
        float movementSpeed = 0.05f + (age / 200.0f);
        if (playerTom == null){
            return;
        }
            // Find the difference between the two players
        Vector3 difference = playerTom.transform.position - transform.position;

        //update the position
        transform.position = transform.position + new Vector3(difference.x * movementSpeed * Time.deltaTime, difference.y * movementSpeed * Time.deltaTime, 0);

        float attackPeriod = Mathf.Max(1.0f, 10.0f - (age / 10.0f));

        if (age - lastAttackTime > attackPeriod){
            lastAttackTime = age;
            Debug.Log("Sharon is attacking!");
            Array values = AttackType.GetValues(typeof(AttackType));
            AttackType randomAttack = (AttackType)values.GetValue(rnd.Next(values.Length));
        switch (randomAttack){
            case AttackType.Alcohol:
                Debug.Log("Sharon is attacking with alcohol!");
                LaunchCocktail();
                break;
            case AttackType.FloorCone:
                Debug.Log("Sharon is attacking with a floor cone!");
                LayFloorCone();
                break;
            case AttackType.MarsBar:
                Debug.Log("Sharon is attacking with a MarsBar!");
                LaunchMarsBar();
                break;
            }
        }
    }

    void LaunchMarsBar()
    {
        Vector3 direction = playerTom.transform.position - transform.position;
        float force = 20;
        GameObject projectile = Instantiate(marsBarPrefab, transform.position + (Vector3)direction * 0.2f, Quaternion.identity);
        // projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force);
       
    }

    void LaunchCocktail()
    {
        Vector3 direction = playerTom.transform.position - transform.position;
        float force = 20;
        GameObject projectile = Instantiate(cocktailPrefab, transform.position + (Vector3)direction * 0.2f, Quaternion.identity);
        // projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force);
       
    }
}

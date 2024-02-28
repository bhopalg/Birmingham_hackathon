using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
public class SharonBehaviour : MonoBehaviour
{
    //movement speed in units per second
    private float movementSpeed = 0.1f;

    private GameObject playerTom;

    public GameObject floorConePrefab;

    private static Random rnd = new Random();

    private float lastAttackTime;
       private float birth = 0.0f;
   private float age = 0.0f;

private enum AttackType {
    Alcohol,
    FloorCone
}

    void Start()
    {
        Debug.Log("Starting Sharon!");
        birth = Time.realtimeSinceStartup;
        lastAttackTime = birth;
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
    this.age=Time.realtimeSinceStartup-birth;
    if (playerTom == null){
        return;
    }
        // Find the difference between the two players
    Vector3 difference = playerTom.transform.position - transform.position;

    //update the position
    transform.position = transform.position + new Vector3(difference.x * movementSpeed * Time.deltaTime, difference.y * movementSpeed * Time.deltaTime, 0);

    if (age - lastAttackTime > 5.0f){
        lastAttackTime = age;
        Debug.Log("Sharon is attacking!");
        Array values = AttackType.GetValues(typeof(AttackType));
        AttackType randomAttack = (AttackType)values.GetValue(rnd.Next(values.Length));
    switch (randomAttack){
        case AttackType.Alcohol:
            Debug.Log("Sharon is attacking with alcohol!");
            MoveTom.instance.MakeDrunk();
            break;
        case AttackType.FloorCone:
            Debug.Log("Sharon is attacking with a floor cone!");
            LayFloorCone();
            break;
        }
    }
    }
}

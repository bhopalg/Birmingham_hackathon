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

    private static Random rnd = new Random();

    private float lastAttackTime = 0.0f;

private enum AttackType {
    Alcohol,
}

    void Start()
    {
        playerTom = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTom == null){
            return;
        }
           // Find the difference between the two players
        Vector3 difference = playerTom.transform.position - transform.position;

        //update the position
        transform.position = transform.position + new Vector3(difference.x * movementSpeed * Time.deltaTime, difference.y * movementSpeed * Time.deltaTime, 0);

        if (Time.realtimeSinceStartup - lastAttackTime > 12.0f){
            lastAttackTime = Time.realtimeSinceStartup;
            Debug.Log("Sharon is attacking!");
            Array values = AttackType.GetValues(typeof(AttackType));
            AttackType randomAttack = (AttackType)values.GetValue(rnd.Next(values.Length));
        switch (randomAttack){
            case AttackType.Alcohol:
                Debug.Log("Sharon is attacking with alcohol!");
                MoveTom.instance.MakeDrunk();
                break;
            }
        }
    }
    }

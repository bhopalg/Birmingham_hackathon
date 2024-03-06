using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
using Unity.VisualScripting;
public class SharonBehaviour : MonoBehaviour
{
    //movement speed in units per second
    private float movementSpeed = 0.1f;

    private GameObject playerTom;

    private static Random rnd = new Random();

    private float lastAttackTime = 0.0f;
    public GameObject chairOne;
    public GameObject chairTwo;
    public GameObject chairThree;
    public GameObject chairFour;
    public GameObject chairFive;

    public float rotationSpeed = 10f;

    private bool groupActive = true;

    private enum AttackType {
        Alcohol,
    }

    void Start()
    {
        playerTom = GameObject.FindGameObjectWithTag("Player");
        // GameObject _chairOne = Instantiate(chairOne, transform.position + (Vector3)transform.up * 2f, Quaternion.identity);

        // float x = transform.position.x + Mathf.Cos(((360 / 5) * 1) + 1f);
        // float y = transform.position.y + Mathf.Sin(((360 / 5) * 1) + 1f);

        // Vector3 newPosition = new Vector3(x, y, 0f);

        // GameObject _chairTwo = Instantiate(chairTwo, newPosition, Quaternion.identity);

        // float xThree = transform.position.x + Mathf.Cos(((360 / 5) * 2) + 1f);
        // float yThree = transform.position.y + Mathf.Sin(((360 / 5) * 2) + 1f);

        // Vector3 newPositionThree = new Vector3(xThree, yThree, 0f);

        // GameObject _chairThree = Instantiate(chairThree, newPositionThree, Quaternion.identity);

        // float xFour = transform.position.x + Mathf.Cos(((360 / 5) * 3) + 1f);
        // float yFour = transform.position.y + Mathf.Sin(((360 / 5) * 3) + 1f);

        // Vector3 newPositionFour = new Vector3(xFour, yFour, 0f);

        // GameObject _chairFour = Instantiate(chairFour, newPositionFour, Quaternion.identity);

        // float xFive = transform.position.x + Mathf.Cos(((360 / 5) * 4) + 1f);
        // float yFive = transform.position.y + Mathf.Sin(((360 / 5) * 5) + 1f);

        // Vector3 newPositionFive = new Vector3(xFive, yFive, 0f);

        // GameObject _chairFive = Instantiate(chairFive, newPositionFive, Quaternion.identity);
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

        float rotationSpeed = 2f;
        float desiredDistanceFromCenter = 2f;

        float angle = Time.time * rotationSpeed; // Adjust based on your needs

        int index = 1;
        
        // foreach (string chair in overallGroup)
        // {
        //     var chairObj = GameObject.Find(chair);

        //     // I want to move the chairs in position around the player

            


        //     // float radius = desiredDistanceFromCenter;

        //     // float x = transform.position.x + Mathf.Cos(((360 / 5) * index) + angle) * radius;
        //     // float y = transform.position.y + Mathf.Sin(((360 / 5) * index) + angle) * radius;

        //     // Vector3 newPosition = new Vector3(x, y, 0f);
        //     // chairObj.transform.position = chairObj.transform.position + newPosition;
        //     index++;
        // }
    }

    void SwitchActiveGroup(Array group, bool active) {
        foreach (string chair in group)
        {
            var chairObj = GameObject.Find(chair);
            chairObj.GetComponent<Renderer>().enabled = active; 
        }
    }
}

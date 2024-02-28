using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharonBehaviour : MonoBehaviour
{
    //movement speed in units per second
    private float movementSpeed = 0.1f;

    private GameObject playerTom;

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
    }

}

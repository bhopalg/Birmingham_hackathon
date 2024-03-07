using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreenEnemyBehaviour : MonoBehaviour
{
    public float radius = 0.01f; // Adjust this value to change the circle's radius
    float angle = 0.0f;
    public float speed = 1.0f; // Adjust this value to change the rotation speed

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed; // Adjust speed for desired rotation speed

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x + -3.75f, y, 0.0f); // Set Z position to 0 for 2D movement
    }
}
